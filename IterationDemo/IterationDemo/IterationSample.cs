using System;
using System.Collections;

namespace IterationDemo
{
    public class IterationSample : IEnumerable
    {
        public readonly int StartingPoint;
        public readonly object[] Values;

        public IterationSample(object[] values, int startingPoint)
        {
            Values = values;
            StartingPoint = startingPoint;
        }

        public IEnumerator GetEnumerator()
        {
            return new IterationSampleEnumerator(this);
        }
    }

    public sealed class IterationSampleEnumerator : IEnumerator
    {
        private readonly IterationSample _parent; //迭代的对象  #1
        private int _position; //当前游标的位置 #2

        internal IterationSampleEnumerator(IterationSample parent)
        {
            _parent = parent;
            _position = -1;
        }

        public bool MoveNext()
        {
            if (_position != _parent.Values.Length) //判断当前位置是否为最后一个，如果不是游标自增 #4
            {
                _position++;
            }

            return _position < _parent.Values.Length;
        }

        public void Reset()
        {
            _position = -1;
        }

        public object Current {
            get
            {
                if (_position == -1 || _position == _parent.Values.Length)//第一个之前和最后一个自后的访问非法 #5
                {
                    throw new InvalidOperationException();
                }

                Int32 index = _position + _parent.StartingPoint;//考虑自定义开始位置的情况  #6
                index = index % _parent.Values.Length;
                return _parent.Values[index];
            }
        }
    }
}