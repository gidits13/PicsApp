using System;
using System.Collections.Generic;
using System.Text;

namespace PicsApp.Models
{
	public class Pic
	{
        private string _pin { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public DateTime CreationTime { get; set; }        
    }
}
