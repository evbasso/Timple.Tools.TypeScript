﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeScriptTools_Test.Sample
{
  public class Class : BaseClass
  {
    public List<IInterface> ListOfIInterface { get; set; }
    public BaseClass[] ArrayOfBaseClass { get { return null; } }
    public eSampleEnum EnumValue { get; set; }
    public Dictionary<String, String> DictionaryOfString { get; set; }
  }
}
