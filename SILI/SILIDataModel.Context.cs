﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SILI
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SILI_DBEntities : DbContext
    {
        public SILI_DBEntities()
            : base("name=SILI_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CodigoPostal> CodigoPostal { get; set; }
        public virtual DbSet<MotivoDevolucao> MotivoDevolucao { get; set; }
        public virtual DbSet<Tarefa> Tarefa { get; set; }
        public virtual DbSet<TipoDevolucao> TipoDevolucao { get; set; }
        public virtual DbSet<Tipologia> Tipologia { get; set; }
        public virtual DbSet<Tratamento> Tratamento { get; set; }
        public virtual DbSet<Morada> Morada { get; set; }
        public virtual DbSet<TipoDevolvedor> TipoDevolvedor { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Destinatario> Destinatario { get; set; }
        public virtual DbSet<LoteProduto> LoteProduto { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<DetalheRecepcao> DetalheRecepcao { get; set; }
        public virtual DbSet<Recepcao> Recepcao { get; set; }
    }
}
