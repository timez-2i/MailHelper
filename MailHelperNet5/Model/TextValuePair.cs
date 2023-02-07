using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailHelperNet5.Model
{
    public class TextValuePair
    {
        string m_text;
        object m_value;

        public string Text { get { return m_text; } }
        public object Value { get { return m_value; } }


        public TextValuePair(string text, object value)
        {
            m_text = text;
            m_value = value;
        }

        public override string ToString()
        {
            return m_value.ToString();
        }

        public string ToText()
        {
            return m_text.ToString();
        }
    }
}
