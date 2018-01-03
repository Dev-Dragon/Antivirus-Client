using System;
using System.Collections.Generic;
using System.Text;

namespace DevDragon.AV.Client
{
    public class ScanResult
    {
        /// <summary>
        /// Unique request Id assigned by Api
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Has Virus been identified in the uploaded file. If it has, read 
        /// </summary>
        public bool HasVirus { get; set; }

        public string Message { get; set; }

        public string ServiceVersion { get; set; }
    }
}
