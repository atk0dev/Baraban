// ***********************************************************************
// Assembly         : WinKAS.Authentication
// Author           : Andrii Tkach
// Created          : 03-12-2012
//
// Last Modified By : Andrii Tkach
// Last Modified On : 03-12-2012
// ***********************************************************************
// <copyright file="ObjectDumper.cs" company="WinKAS A/S">
//     WinKAS A/S. All rights reserved.
// </copyright>
// <summary>
//    Object Dumper
// </summary>
// ***********************************************************************

namespace ShowcaseED.Utils
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Object Dumper tool
    /// </summary>
    public class ObjectDumper
    {
        /// <summary>
        /// The writer
        /// </summary>
        private TextWriter writer;

        /// <summary>
        /// The pos
        /// </summary>
        private int pos;

        /// <summary>
        /// The level
        /// </summary>
        private int level;

        /// <summary>
        /// The depth
        /// </summary>
        private int depth;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectDumper" /> class.
        /// </summary>
        /// <param name="depth">The depth.</param>
        private ObjectDumper(int depth)
        {
            this.depth = depth;
        }

        /// <summary>
        /// Writes the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        public static void Write(object element)
        {
            Write(element, 0);
        }

        /// <summary>
        /// Writes the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="depth">The depth.</param>
        public static void Write(object element, int depth)
        {
            Write(element, depth, Console.Out);
        }

        /// <summary>
        /// Writes the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="log">The log.</param>
        public static void Write(object element, int depth, TextWriter log)
        {
            ObjectDumper dumper = new ObjectDumper(depth);
            dumper.writer = log;
            dumper.WriteObject(null, element);
        }

        /// <summary>
        /// Writes as text.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>String with the object</returns>
        public static string WriteAsText(object element)
        {
            var stringBuilder = new StringBuilder();
            using (TextWriter writer = new StringWriter(stringBuilder))
            {
                Write(element, 0, writer);
            }

            string fullString = stringBuilder.ToString();
            return fullString;
        }

        /// <summary>
        /// Writes the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        private void Write(string s)
        {
            if (s != null)
            {
                this.writer.Write(s);
                this.pos += s.Length;
            }
        }

        /// <summary>
        /// Writes the indent.
        /// </summary>
        private void WriteIndent()
        {
            for (int i = 0; i < this.level; i++)
            {
                this.writer.Write("  ");
            }
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        private void WriteLine()
        {
            this.writer.WriteLine();
            this.pos = 0;
        }

        /// <summary>
        /// Writes the tab.
        /// </summary>
        private void WriteTab()
        {
            Write("  ");
            while (this.pos % 8 != 0)
            {
                Write(" ");
            }
        }

        /// <summary>
        /// Writes the object.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="element">The element.</param>
        private void WriteObject(string prefix, object element)
        {
            if (element == null || element is ValueType || element is string)
            {
                this.WriteIndent();
                this.Write(prefix);
                this.WriteValue(element);
                this.WriteLine();
            }
            else
            {
                IEnumerable enumerableElement = element as IEnumerable;
                if (enumerableElement != null)
                {
                    foreach (object item in enumerableElement)
                    {
                        if (item is IEnumerable && !(item is string))
                        {
                            this.WriteIndent();
                            Write(prefix);
                            Write("...");
                            this.WriteLine();
                            if (this.level < this.depth)
                            {
                                this.level++;
                                this.WriteObject(prefix, item);
                                this.level--;
                            }
                        }
                        else
                        {
                            this.WriteObject(prefix, item);
                        }
                    }
                }
                else
                {
                    MemberInfo[] members = element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);
                    this.WriteIndent();
                    Write(prefix);
                    bool propWritten = false;
                    foreach (MemberInfo m in members)
                    {
                        FieldInfo f = m as FieldInfo;
                        PropertyInfo p = m as PropertyInfo;
                        if (f != null || p != null)
                        {
                            if (propWritten)
                            {
                                this.WriteTab();
                            }
                            else
                            {
                                propWritten = true;
                            }

                            Write(m.Name);
                            Write("=");
                            Type t = f != null ? f.FieldType : p.PropertyType;
                            if (t.IsValueType || t == typeof(string))
                            {
                                this.WriteValue(f != null ? f.GetValue(element) : p.GetValue(element, null));
                            }
                            else
                            {
                                if (typeof(IEnumerable).IsAssignableFrom(t))
                                {
                                    Write("...");
                                }
                                else
                                {
                                    Write("{ }");
                                }
                            }
                        }
                    }

                    if (propWritten)
                    {
                        this.WriteLine();
                    }

                    if (this.level < this.depth)
                    {
                        foreach (MemberInfo m in members)
                        {
                            FieldInfo f = m as FieldInfo;
                            PropertyInfo p = m as PropertyInfo;
                            if (f != null || p != null)
                            {
                                Type t = f != null ? f.FieldType : p.PropertyType;
                                if (!(t.IsValueType || t == typeof(string)))
                                {
                                    object value = f != null ? f.GetValue(element) : p.GetValue(element, null);
                                    if (value != null)
                                    {
                                        this.level++;
                                        this.WriteObject(m.Name + ": ", value);
                                        this.level--;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="o">The o.</param>
        private void WriteValue(object o)
        {
            if (o == null)
            {
                Write("null");
            }
            else if (o is DateTime)
            {
                Write(((DateTime)o).ToShortDateString());
            }
            else if (o is ValueType || o is string)
            {
                Write(o.ToString());
            }
            else if (o is IEnumerable)
            {
                Write("...");
            }
            else
            {
                Write("{ }");
            }
        }
    }
}