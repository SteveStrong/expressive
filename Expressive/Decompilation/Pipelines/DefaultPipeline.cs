﻿using System;
using System.Collections.Generic;
using System.Linq;

using AshMind.Extensions;

using Expressive.Decompilation.Steps;
using Expressive.Decompilation.Steps.Clarity;
using Expressive.Decompilation.Steps.IndividualElements;
using Expressive.Decompilation.Steps.StatementInlining;

namespace Expressive.Decompilation.Pipelines {
    public class DefaultPipeline : DecompilationPipeline {
        public DefaultPipeline() : base(
            new NopRemovalStep(),
            new BrCuttingStep(),
            new BrResolutionStep(),
            new CutBranchesRemovalStep(),
            new IndividualDecompilationStep(
                new LdfldToField(),
                new LdstrToConstant(),
                new CxxToCondition(),
                new CallToExpression(),
                new LdlocToVariable(),
                new StlocToAssignment(),
                new LdargToParameter(),
                new LdcToConstant(),
                new BranchToCondition(),
                new RetToReturn()
            ),
            new VisitorSequenceStep(
                new IfAssignmentInliningVisitor(),
                new IfReturnInliningVisitor(),
                new ConditionImprovementVisitor(),
                new BooleanEqualityImprovementVisitor(),
                new NotImprovementVisitor()
            ),
            new VariableInliningStep()
        ) {
        }

        public virtual DefaultPipeline Without<TPipelinePart>() {
            this.Steps.RemoveWhere(s => s is TPipelinePart);
            this.Steps.ForEach(this.RemoveFromStep<TPipelinePart>);

            return this;
        }

        protected virtual void RemoveFromStep<TPipelinePart>(IDecompilationStep step) {
            var individual = step as IndividualDecompilationStep;
            if (individual != null) {
                individual.Interpretations.RemoveWhere(i => i is TPipelinePart);
                return;
            }

            var sequence = step as VisitorSequenceStep;
            if (sequence != null) {
                sequence.Visitors.RemoveWhere(v => v is TPipelinePart);
                return;
            }
        }
    }
}
