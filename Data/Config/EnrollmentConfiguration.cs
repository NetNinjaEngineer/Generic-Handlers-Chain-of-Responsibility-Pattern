﻿using ApplyingGenericRepositoryPattern.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentManagement.API.Data.Config;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.Property(x => x.StudentMark)
            .IsRequired(false);

        builder.HasKey(x => new { x.StudentId, x.CourseId });

        builder.ToTable("Enrollment");
    }
}
