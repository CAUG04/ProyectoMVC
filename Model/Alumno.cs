namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Alumno")]
    public partial class Alumno
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Alumno()
        {
            Adjunto = new HashSet<Adjunto>();
            AlumnoCurso = new HashSet<AlumnoCurso>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        public int Sexo { get; set; }

        [Required]
        [StringLength(10)]
        public string FechaNacimiento { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Adjunto> Adjunto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlumnoCurso> AlumnoCurso { get; set; }


        public AnexGRIDResponde Listar(AnexGRID grid)
        {
            try
            {
                using (var ctx = new TetsContext())
                {
                    grid.Inicializar();

                    var query = ctx.Alumno.Where(x => x.id > 0);

                    // Ordenamiento
                    if (grid.columna == "id")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.id)
                                                             : query.OrderBy(x => x.id);
                    }

                    if (grid.columna == "Nombre")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.Nombre)
                                                             : query.OrderBy(x => x.Nombre);
                    }

                    if (grid.columna == "Sexo")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.Sexo)
                                                             : query.OrderBy(x => x.Sexo);
                    }

                    if (grid.columna == "FechaNacimiento")
                    {
                        query = grid.columna_orden == "DESC" ? query.OrderByDescending(x => x.FechaNacimiento)
                                                             : query.OrderBy(x => x.FechaNacimiento);
                    }

                    var alumnos = query.Skip(grid.pagina)
                                       .Take(grid.limite)
                                       .ToList();

                    var total = query.Count();

                    grid.SetData(
                        from a in alumnos
                        select new
                        {
                            a.id,
                            a.Nombre,
                            a.Sexo,
                            a.FechaNacimiento
                        },
                        total
                    );
                }
            }
            catch (Exception E)
            {

                throw;
            }

            return grid.responde();
        }

        public Alumno Obtener(int id)
        {
            var alumno = new Alumno();

            try
            {
                using (var ctx = new TetsContext())
                {
                    alumno = ctx.Alumno.Include("AlumnoCurso")
                        .Include("AlumnoCurso.Curso")
                        .Where(x => x.id == id)
                        .SingleOrDefault();
                }
            }
            catch (Exception exception)
            {
                throw;
            }
            return alumno;
        }

        public ResponseModel Guardar()
        {
            var rm = new ResponseModel();
            try
            {
                using (var ctx = new TetsContext())
                {
                    if (this.id > 0)
                    {
                        ctx.Entry(this).State = EntityState.Modified;
                    }
                    else
                    {
                        ctx.Entry(this).State = EntityState.Added;

                    }
                    rm.SetResponse(true);
                    ctx.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                throw;
            }

            return rm;
        }

        public void Eliminar()
        {
            try
            {
                using (var ctx = new TetsContext())
                {

                    ctx.Entry(this).State = EntityState.Deleted;
                    ctx.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
