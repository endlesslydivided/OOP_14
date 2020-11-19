using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_14_1
{
    interface IActionable
    {
        void Forward();
        void Attack();
    }

    interface ISerializable
    {
        void SerializeWithAllWays();
        void DeserializeWithAllWays();
    }
}
