using System;
using System.Collections.Generic;
using System.Text;

namespace Guessing_game.UI
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class DisplayTextAttribute : Attribute
    {
        public string Text { get; }
        public DisplayTextAttribute(string text)
        {
            Text = text;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]

    public class PromptAttribute : Attribute
    {
        public string Prompt { get; }
        public PromptAttribute(string prompt)
        {
            Prompt = prompt;
        }
    }
}