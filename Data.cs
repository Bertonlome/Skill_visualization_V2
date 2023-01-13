﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skill_visualization
{
    public class Collab
    {
        //[PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime DateSave { get; set; }
        public string Name { get; set; }
        public string Resume { get; set; }

       // [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Project> Projects { get; set; }
    }

    public class Project
    {
        //[PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        //[ForeignKey(typeof(Collab))]
        public int DataId { get; set; }
        //[ManyToOne]
        public Collab Collab { get; set; }
        public DateTime DateSave { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Skill
    {
        //[PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        //[ForeignKey(typeof(Collab))]
        public int DataId { get; set; }
        //[ManyToOne]
        public Collab Collab { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

}
