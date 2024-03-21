using System;
using System.Collections.Generic;
using System.Text;

namespace DoFactory.Framework.Controls
{
    /// <summary>
    /// A menu item. This is a node in a tree of menu items. Menu items
    /// can have children themselves.
    /// 
    /// GoF Design Pattern: Composite.
    /// </summary>
    [Serializable()]
    public class MenuCompositeItem
    {
        private List<MenuCompositeItem> _children = new List<MenuCompositeItem>();
        private string _item;
        private string _link;

        /// <summary>
        /// Constructor of menu item.
        /// </summary>
        /// <param name="item">Menu item display name.</param>
        /// <param name="link">Menu item link</param>
        public MenuCompositeItem(string item, string link)
        {
            this._item = item;
            this._link = link;
        }

        /// <summary>
        /// Gets and set menu item display name.
        /// </summary>
        public string Item
        {
            get { return _item; }
            set { _item = value; }
        }

        /// <summary>
        /// Gets and sets menu item link.
        /// </summary>
        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }

        /// <summary>
        /// Gets and set list of child menu items.
        /// Composite Design Pattern. It is trought the Children property 
        /// that an item can reference an entire list of the same objects. 
        /// </summary>
        public List<MenuCompositeItem> Children
        {
            get { return _children; }
            set { _children = value; }
        }
    }
}
