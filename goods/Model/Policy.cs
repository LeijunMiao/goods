using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods.Model
{
    class PolicyModel
    {
        private int id;
        private int role; 
        private int feature;

        public PolicyModel() { }
        public PolicyModel(int roleId,int feature) {
            this.role = roleId;
            this.feature = feature;
        }
        public int Id
        {
            set
            {
                id = value;
            }
            get
            {
                return id;
            }
        }
        public int Feature
        {
            set
            {
                feature = value;
            }
            get
            {
                return feature;
            }
        }
        public int Role
        {
            set
            {
                role = value;
            }
            get
            {
                return role;
            }
        }

    }

    class FeatureModel
    {
        private int id;
        private string name;

        public FeatureModel() { }
        public int Id
        {
            set
            {
                id = value;
            }
            get
            {
                return id;
            }
        }
        public string Name
        {
            set
            {
                name = value;
            }
            get
            {
                return name;
            }
        }
    }

}
