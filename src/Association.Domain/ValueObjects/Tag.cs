﻿using System.Collections.Generic;
using Common.Domain.ValueObjects;

namespace Association.Domain.ValueObjects
{
    public class Tag : ValueObject
    {
        private readonly string _username;
        private readonly int _number;

        public Tag(string username, int number)
        {
            _username = username;
            _number = number;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return _username;
            yield return _number;
        }
    }
}