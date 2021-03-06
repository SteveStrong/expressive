﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Expressive.Matching;

namespace Expressive.Elements.Expressions.Matchers {
    public static class MatcherMemberExtensions {
        public static Matcher<MemberExpression> Property(
            this Matcher<MemberExpression> matcher,
            Func<PropertyInfo, bool> matchProperty
        ) {
            return matcher.Match(target => {
                var property = target.Member as PropertyInfo;
                return property != null && matchProperty(property);
            });
        }

        public static Matcher<FieldInfo> Field(this Matcher<MemberExpression> matcher) {
            return matcher.For(m => m.Member).As<FieldInfo>();
        }
    }
}
