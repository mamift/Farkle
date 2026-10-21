// C# 1.0 style source used as a parser sample. This file is not compiled.
#region Usings
using System;
using Alias = System.Collections.ArrayList;
#endregion

[assembly: CLSCompliant(true)]

namespace Sample.Declarations
{
    [Serializable]
    public class Widget : BaseWidget, IWidget
    {
        public const int DefaultSize = 10;
        private static readonly object Sync = null;
        protected volatile int state;
        internal Widget[] children;

        public Widget() : this(DefaultSize) { }
        public Widget(int size) : base() { state = size; }
        static Widget() { }
        ~Widget() { }

        public int Size
        {
            get { return state; }
            set { state = value; }
        }

        public int this[int index]
        {
            get { return children[index].state; }
            set { children[index].state = value; }
        }

        public event EventHandler Changed;
        public event EventHandler Clicked
        {
            add { Changed += value; }
            remove { Changed -= value; }
        }

        public void Draw() { }
        public virtual int Measure(int x) { return x; }
        public abstract void Abstract();
        public extern static void External();

        public static Widget operator +(Widget a, Widget b) { return a; }
        public static implicit operator int(Widget w) { return w.state; }
        public static explicit operator Widget(int x) { return null; }
        public static bool operator true(Widget w) { return false; }
        public static bool operator false(Widget w) { return false; }

        void IWidget.Draw() { }
        int IWidget.Size { get { return state; } }
        int IWidget.this[int i] { get { return i; } }
        event EventHandler IWidget.Changed { add { } remove { } }

        class Nested { }
        enum Mode { A = 1, B, }
        delegate void Callback(int x);
    }

    public interface IWidget
    {
        void Draw();
        int Size { get; }
        int this[int i] { get; }
        event EventHandler Changed;
    }

    public struct Point : IComparable
    {
        public int X;
        public int Y;
        public Point(int x, int y) { X = x; Y = y; }
        public int CompareTo(object o) { return 0; }
    }

    public enum Color : byte { Red = 1, Green, Blue }

    public delegate int Transformer(int x);

    unsafe struct Buffer
    {
        public fixed byte Data[16];
    }
}
