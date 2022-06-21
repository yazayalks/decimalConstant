using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecimalConstant
{
    public class RingStack<T>
    {
        private int start;
        private int end;
        private int size;
        private T[] stack;

        public RingStack(int size)
        {
            stack = new T[size];
            this.size = size;
            start = end = 0;
        }

        public void Push(T data)
        {
            stack[end] = data;
            end = (end + 1) % size;
            if (end == start) start = (start + 1) % size;
        }


        public T Pop()
        {
            if (start == end) return default(T);
            if (--end < 0) end = size - 1;
            return stack[end];
        }

        public bool Isempty()
        {
            return (start == end);
        }

        public void Clear()
        {
            start = end = 0;
        }

    }
}
