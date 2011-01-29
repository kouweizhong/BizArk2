﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace BizArk.Core.Util
{

    /// <summary>
    /// Provides methods that are useful when working with properties.
    /// </summary>
    public static class PropertyUtil
    {

        /// <summary>
        /// Gets the name of the property based on a Linq expression.
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="propertyRefExpr"></param>
        /// <returns></returns>
        public static string GetName<TObject>(Expression<Func<TObject, object>> propertyRefExpr)
        {
            return GetNameCore(propertyRefExpr.Body);
        }

        public static string GetNameCore(Expression propertyRefExpr)
        {
            if (propertyRefExpr == null)
                throw new ArgumentNullException("propertyRefExpr", "propertyRefExpr is null.");

            MemberExpression memberExpr = propertyRefExpr as MemberExpression;
            if (memberExpr == null)
            {
                UnaryExpression unaryExpr = propertyRefExpr as UnaryExpression;
                if (unaryExpr != null && unaryExpr.NodeType == ExpressionType.Convert)
                    memberExpr = unaryExpr.Operand as MemberExpression;
            }

            if (memberExpr != null && memberExpr.Member.MemberType == MemberTypes.Property)
                return memberExpr.Member.Name;

            throw new ArgumentException("No property reference expression was found.", "propertyRefExpr");
        }

    }
}
