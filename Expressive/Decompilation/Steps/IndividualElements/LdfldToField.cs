﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;

using ClrTest.Reflection;
using Expressive.Elements;

namespace Expressive.Decompilation.Steps.IndividualElements {
    public class LdfldToField : ElementInterpretation<InstructionElement, ExpressionElement> {
        public override bool CanInterpret(InstructionElement instruction) {
            return instruction.OpCode == OpCodes.Ldfld
                || instruction.OpCode == OpCodes.Ldsfld;
        }

        public override ExpressionElement Interpret(InstructionElement instruction, IndividualDecompilationContext context) {
            var field = ((InlineFieldInstruction)instruction.Instruction).Field;
            var instance = !field.IsStatic
                         ? context.CapturePreceding<ExpressionElement>().Expression
                         : null;
            return new ExpressionElement(Expression.Field(instance, field));
        }
    }
}