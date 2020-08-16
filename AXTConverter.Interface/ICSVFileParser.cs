using System;
using System.Collections.Generic;
using System.Text;

namespace AXTConverter.Interface
{
    public interface ICSVFileParser
    {
        void TransformCSV(string filePath);
    }
}
