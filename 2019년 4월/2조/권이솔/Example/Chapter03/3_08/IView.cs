﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03._3_8
{
    public interface IView
    {
        void Close();
    }

    public partial class Form1 : Form, IView
    {
        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}
