﻿using System;
namespace BusinessLogic
{
    public class Pair<k, v>
    {
        private k key;
        private v value;

        public Pair()
        {
        }
        public Pair(k key, v value)
        {
            this.Key = key;
            this.Value = value;
        }

        public k Key { get => key; set => key = value; }
        public v Value { get => value; set => this.value = value; }
    }
}
