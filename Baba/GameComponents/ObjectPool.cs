using System;

namespace Baba.GameComponents
{
    //Thanks chat-gpt!
    public class ObjectPool
    {
        private readonly object[] _pool;
        private readonly int _maxSize;
        private int _head;
        private int _tail;
        private Type _type;

        public ObjectPool(int maxSize, Type type)
        {
            _type = type;
            _maxSize = maxSize;
            _pool = new object[maxSize];
            _head = 0;
            _tail = 0;
        }

        public object GetObject()
        {
            if (_head == _tail && _pool[_head] == null)
            {
                return new object();
            }
            var obj = _pool[_head];
            _pool[_head] = null;
            _head = (_head + 1) % _maxSize;
            return obj;
        }

        public void ReturnObject(object obj)
        {
            if (_pool[_tail] != null)
            {
                //Discard the object if the pool is full
            }
            _pool[_tail] = obj;
            _tail = (_tail + 1) % _maxSize;
        }
    }
}
