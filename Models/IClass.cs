using System;
using System.Collections.Generic;  

namespace Models
{
    public interface IClass{

        /// <returns> a string containing the name of the class.</returns>
        string Identify();
        /// It will return a List<string> containing each of its variables displayed for the menubuilder.
        /// <returns>{$"name:{name}","phone:{phone}","color:{color}"}</returns>
        List<string> ToStringList();
    }

}