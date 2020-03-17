using System;
using System.Collections.Generic;
using System.Data;

namespace GameLogic
{
    public interface IMineMapBuilder
    {
        int Rows { get; }
        int Columns { get;}

        
        List<Tuple<int, int>> Build(); 
    }
}