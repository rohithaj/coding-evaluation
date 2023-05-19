using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;
        private static int _id = 0;
        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? Hire(Name person, string title)
        {
            return Hire(person, title, root);
        }
        private Position? Hire(Name person, string title, Position position)
        {
            if (position == null)
                return null;
            if (position.GetTitle() == title)
            {
                _id++;
                position.SetEmployee(new Employee(_id, person));
                return position;
            }
            else
            {
                foreach (var item in position.GetDirectReports())
                {
                    var pos = Hire(person, title, item);
                    if (pos != null)
                        return pos;
                }
            }
            return null;
        }

        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "\t"));
            }
            return sb.ToString();
        }
    }
}
