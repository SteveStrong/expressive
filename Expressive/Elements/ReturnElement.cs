﻿using System;
using System.Collections.Generic;
using System.Linq;

using Expressive.Elements.Presentation;

namespace Expressive.Elements {
    public class ReturnElement : IElement {
        public IElement Result { get; private set; }

        public ReturnElement() : this(null) {
        }

        public ReturnElement(IElement result) {
            this.Result = result;
        }

        public override string ToString() {
            return "return" + (this.Result != null ? " " + this.Result : "");
        }

        public string ToString(Indent indent) {
            return indent.Value + this;
        }
    }
}
