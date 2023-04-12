using System;

namespace Baba.GameComponents
{
    //Thanks chat-gpt!
    public class ObjectPool
    {
        private readonly object[] _pool;
        private readonly Type _objectType;
        private int _head;
        private int _tail;
        private int _count;

        public ObjectPool(int capacity, Type objectType)
        {
            _pool = new object[capacity];
            _objectType = objectType;
            InitializePool();
        }

        public T GetObject<T>() where T : class
        {
            if (_count == 0)
            {
                return CreateObject<T>();
            }
            _count--;
            var obj = (T)_pool[_tail];
            _pool[_tail] = null;
            _tail = (_tail + 1) % _pool.Length;
            return obj;
        }

        public void ReturnObject(object obj)
        {
            if (_count == _pool.Length)
            {
                return;
            }
            _count++;
            _pool[_head] = obj;
            _head = (_head + 1) % _pool.Length;
        }

        private void InitializePool()
        {
            for (int i = 0; i < _pool.Length; i++)
            {
                _pool[i] = CreateObject();
                _count++;
            }
            _head = _pool.Length - 1;
            _tail = 0;
        }

        private object CreateObject()
        {
            return Activator.CreateInstance(_objectType);
        }

        private T CreateObject<T>() where T : class
        {
            return CreateObject() as T;
        }
    }
}
