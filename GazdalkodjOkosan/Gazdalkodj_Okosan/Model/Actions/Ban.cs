﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GazdalkodjOkosan.Model.Actions
{
    class Ban : IAction
    {
        public string Message
        {
            get { throw new NotImplementedException(); }
        }

        public bool Cond(Control.IController engine)
        {
            throw new NotImplementedException();
        }

        public void Do(Control.IController engine)
        {
            throw new NotImplementedException();
        }

        //
        private int[] untilRoll;
        private int rounds;
    }
}