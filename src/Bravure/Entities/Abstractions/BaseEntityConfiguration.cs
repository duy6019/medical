using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bravure.Entities.Abstractions
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        protected EntityTypeBuilder<T> builder { get; private set; }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            this.builder = builder;

            this.SetTable()
                .SetPrimaryKey()
                .SetPKColumnName()
                .SetPkGuid();

            if (ConfigureSequence())
                this.HasSequence();

            Configure();
        }

        public abstract void Configure();

        public virtual bool ConfigureSequence() => typeof(T).IsSubclassOf(typeof(IdentifiableEntity<int>));

        /// <summary>
        /// Custom PK column name
        /// </summary>
        /// <returns>If true: will set PK column Name = TABLENAME + Id; If false: PK column is Id</returns>
        protected virtual bool UseCustomPkNaming() => true;

        protected virtual bool SetPKRowGuid() => typeof(T).IsSubclassOf(typeof(IdentifiableEntity<Guid>));

        protected virtual string GetPrimaryKeyName() => nameof(IdentifiableEntity<Guid>.Id);

        protected virtual string GetCustomPKName() => typeof(T).Name + GetPrimaryKeyName();

        protected virtual BaseEntityConfiguration<T> SetTable()
        {
            builder.ToTable(typeof(T).Name, BravureDbContext.DEFAULT_SCHEMA);
            return this;
        }

        protected virtual BaseEntityConfiguration<T> SetPrimaryKey()
        {
            builder.HasKey(GetPrimaryKeyName());
            return this;
        }

        protected virtual BaseEntityConfiguration<T> SetPKColumnName()
        {
            if (UseCustomPkNaming())
                builder.Property(GetPrimaryKeyName()).HasColumnName(GetCustomPKName());
            return this;
        }

        protected virtual BaseEntityConfiguration<T> SetPkGuid()
        {
            if (SetPKRowGuid())
                builder.Property(GetPrimaryKeyName()).HasDefaultValueSql("NEWSEQUENTIALID()");

            return this;
        }

        protected virtual BaseEntityConfiguration<T> HasSequence()
        {
            builder.Property(GetPrimaryKeyName()).UseHiLo(GetSequenceName(), BravureDbContext.DEFAULT_SCHEMA);
            return this;
        }

        internal virtual BaseEntityConfiguration<T> HasRequiredField<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            builder.Property(propertyExpression).IsRequired();
            return this;
        }

        internal virtual BaseEntityConfiguration<T> HasIgnoredField(Expression<Func<T, object>> properyExpression)
        {
            builder.Ignore(properyExpression);
            return this;
        }

        internal virtual BaseEntityConfiguration<T> HasQueryFilter(Expression<Func<T, bool>> filter)
        {
            builder.HasQueryFilter(filter);
            return this;
        }

        internal virtual BaseEntityConfiguration<T> HasParent<TParent>(
            Expression<Func<T, TParent>> parentExpression,
            Expression<Func<TParent, IEnumerable<T>>> navigationExpression,
            Expression<Func<T, object>> foreignKeyExpression)
            where TParent : BaseEntity
        {
            builder.HasOne(parentExpression).WithMany(navigationExpression).HasForeignKey(foreignKeyExpression);
            return this;
        }

        internal virtual BaseEntityConfiguration<T> HasOne<TParent>(
            Expression<Func<T, TParent>> parentExpression,
            Expression<Func<TParent, T>> navigationExpression,
            Expression<Func<T, object>> foreignKeyExpression)
            where TParent : BaseEntity
        {
            builder.HasOne(parentExpression).WithOne(navigationExpression).HasForeignKey(foreignKeyExpression);
            return this;
        }

        internal static string GetSequenceName() => $"{nameof(T)}DBSequenceHiLo";
    }
}
