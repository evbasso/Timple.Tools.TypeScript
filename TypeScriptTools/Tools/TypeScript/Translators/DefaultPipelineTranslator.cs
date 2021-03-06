﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timple.Tools.TypeScript.Translators
{
  public class DefaultPipelineTranslator : ITypeScriptPipelineTranslator
  {
    public DefaultPipelineTranslator() {

    }

    public virtual bool Accepts(Type tp) {
      return true;
    }

    public virtual string Translate(Type tp, TypeScriptPipelineTranslator translator) {
      String name;
      String fullName;
      Type toTranslate;

      toTranslate = tp;

      if (tp.IsArray) {
        return translator.Translate(tp.GetElementType()) + "[]";
      }

      if (tp == typeof(void))
        return "void";

      if (tp.IsGenericType) {
        name = tp.Name.Split('`').First() + "<" + String.Join(", ", tp.GetGenericArguments().Select(x => translator.Translate(x))) + ">";
        fullName = tp.Namespace + "." + name;
        if (!tp.IsGenericTypeDefinition) {
          translator.Translate(tp.GetGenericTypeDefinition());
          return fullName;
        }

      } else {
        name = tp.Name;
        fullName = tp.FullName;
      }

      if (tp == typeof(Object))
        return "any";

      translator.RegisterType(tp.FullName, fullName);
      PropertiesOnlyTemplate templ = new PropertiesOnlyTemplate(name, toTranslate, translator, tp.Namespace.StartsWith("System"));
      translator.Writer.Write(templ.TransformText());
      return fullName;
    }

    public virtual void Prepare(TypeScriptPipelineTranslator translator) {
      translator.AddTailTranslator(this);
    }
  }
}
