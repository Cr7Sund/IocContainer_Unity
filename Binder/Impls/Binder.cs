namespace Cr7Sund.IocContainer
{

    public class Binder : IBinder
    {
        /// A handler for resolving the nature of a binding during chained commands
        public delegate void BindingResolver(IBinding binding, object oldName = null);
        protected Dictionary<Type, List<IBinding>> _bindings; // object is implicitly equal to type
        protected BindingResolver _bindingResolverHandler;


        public Binder()
        {
            _bindings = new Dictionary<Type, List<IBinding>>();
            _bindingResolverHandler = Resolver;
        }

        #region IBinder implementation
        public IBinding Bind<T>()
        {
            return Bind(typeof(T));
        }

        public IBinding Bind(Type key)
        {
            IBinding binding;
            binding = GetRawBinding();
            binding.Bind(key);
            return binding;
        }

        protected virtual IBinding GetRawBinding()
        {
            return new Binding(_bindingResolverHandler);
        }

        private void Resolver(IBinding binding, object oldName)
        {
            var key = binding.Key;
            ResolveBinding(binding, binding.Key);
        }

        public IBinding GetBinding<T>(object name)
        {
            var key = typeof(T);
            return GetBinding(key, name);
        }

        public IBinding GetBinding<T>()
        {
            var key = typeof(T);
            return GetBinding(key, null);
        }

        public IBinding GetBinding(Type key)
        {
            return GetBinding(key, null);
        }

        public IBinding GetBinding(Type key, object name)
        {
            if (_bindings.TryGetValue(key, out var list))
            {
                name = name == null ? BindingConst.NULLOIDNAME : name;

                for (int i = 0; i < list.Count; i++)
                {
                    var item = list[i];
                    if (item.Name.Equals(name))
                    {
                        return item;
                    }
                }
            }

            return null;
        }
        public void Unbind<T>()
        {
            Unbind(typeof(T), null);
        }

        public void Unbind(Type key)
        {
            Unbind(key, null);
        }

        public void Unbind<T>(object name = null)
        {
            Unbind(typeof(T), name);
        }

        public void Unbind(IBinding binding)
        {
            if (binding == null)
            {
                return;
            }
            Unbind(binding.Key, binding.Name);
        }

        public void Unbind(Type key, object name)
        {
            if (TryUnbindInternally(key, name, out var binding))
            {
                OnUnbind(binding);
            }
        }

        public void RemoveValue(IBinding binding, object value)
        {
            if (binding == null || value == null)
            {
                return;
            }
            var key = binding.Key;
            if (_bindings.TryGetValue(key, out var dict))
            {
                for (int i = dict.Count - 1; i >= 0; i--)
                {
                    UpdateBindingAndCleanup(binding, value, dict, i);
                }
            }

            void UpdateBindingAndCleanup(IBinding binding, object value, List<IBinding> dict, int i)
            {
                var useBinding = dict[i];
                if (useBinding.Name.Equals(binding.Name))
                {
                    useBinding.RemoveValue(value);

                    //If result is empty, clean it out
                    if (useBinding.Value.Count == 0)
                    {
                        dict.RemoveAt(i);
                    }
                }
            }
        }

        public virtual void ResolveBinding(IBinding binding, Type key, object oldName = null)
        {
            object bindingName = binding.Name;
            object removeName = oldName == null ? BindingConst.NULLOIDNAME : oldName;

            if (_bindings.TryGetValue(key, out var list))
            {
                RemoveConflictingBinding(binding, bindingName, list);
            }
            else
            {
                list = new List<IBinding>();
                _bindings[key] = list;
            }

            RemoveBindingByOldName(binding, removeName, list);
            AddBindingIfNotExists(binding, list, bindingName);

            void RemoveConflictingBinding(IBinding binding, object bindingName, List<IBinding> list)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    var existingBinding = list[i];
                    if (existingBinding.Name != bindingName) continue;
                    if (existingBinding != binding)
                    {
                        if (!existingBinding.IsWeak)
                        {
                            throw new Exception($"{BinderExceptionType.CONFLICT_IN_BINDER}: there exist same binding key: {binding.Key} name: {binding.Name} ");
                        }

                        list.RemoveAt(i);
                        break;
                    }
                }
            }

            void RemoveBindingByOldName(IBinding binding, object removeName, List<IBinding> list)
            {
                //Remove nulled bindings
                // e.g. when we toname to change the binding name but the binderDict still exist the oldName
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    var existingBinding = list[i];
                    if (existingBinding.Name.Equals(removeName) && existingBinding == binding)
                    {
                        list.RemoveAt(i);
                        break;
                    }
                }
            }

            void AddBindingIfNotExists(IBinding binding, List<IBinding> list, object bindingName)
            {
                bool shouldAdd = true;
                for (int i = 0; i < list.Count; i++)
                {
                    var existingBinding = list[i];
                    if (existingBinding.Name.Equals(bindingName))
                    {
                        shouldAdd = false;
                        break;
                    }
                }
                if (shouldAdd)
                {
                    list.Add(binding);
                }
            }
        }

        public virtual void OnRemove()
        {
        }

        public void CopyFrom(IBinder fromBinder)
        {
            var implBinder = fromBinder as Binder;
            foreach (var item in implBinder._bindings)
            {
                foreach (var binding in item.Value)
                {
                    ResolveBinding(binding, item.Key);
                }
            }
        }

        public void RemoveAll()
        {
            foreach (var item in _bindings)
            {
                List<IBinding> bindings = item.Value;
                // foreach (IBinding binding in bindings)
                // {
                //     OnUnbind(binding);
                // }
                bindings.Clear();
            }
            _bindings.Clear();
        }

        public virtual void Dispose()
        {

        }

        public virtual bool TryUnbindInternally(Type key, object name, out IBinding result)
        {
            result = null;

            if (_bindings.TryGetValue(key, out var list))
            {
                name = name == null ? BindingConst.NULLOIDNAME : name;

                for (int i = list.Count - 1; i >= 0; i--)
                {
                    var item = list[i];
                    if (item.Name.Equals(name))
                    {
                        result = item;
                        list.RemoveAt(i);

                        if (list.Count == 0)
                        {
                            _bindings.Remove(key);
                        }
                        break;
                    }
                }
            }
            return result != null;
        }

        protected virtual void OnUnbind(IBinding binding)
        {

        }
        #endregion
    }

}