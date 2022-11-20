
using EjercicioA2_4.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioA2_4.Controller
{
    public class bdatos
    {
        readonly SQLiteAsyncConnection dbase;

        public bdatos(string path)
        {
            dbase = new SQLiteAsyncConnection(path);

            dbase.CreateTableAsync<Video>();
        }

        public bdatos()
        {
        }

        #region OperacionesVideo
        //Metodos CRUD - CREATE
        public Task<int> insertUpdateVideo(Video video)
        {
            if (video.id != 0)
            {
                return dbase.UpdateAsync(video);
            }
            else
            {
                return dbase.InsertAsync(video);
            }
        }

        //Metodos CRUD - READ
        public Task<List<Video>> getListVideo()
        {
            return dbase.Table<Video>().ToListAsync();
        }

        public Task<Video> getVideo(int id)
        {
            return dbase.Table<Video>()
                .Where(i => i.id == id)
                .FirstOrDefaultAsync();
        }

        //Metodos CRUD - DELETE
        public Task<int> deleteVideo(Video video)
        {
            return dbase.DeleteAsync(video);
        }

        #endregion OperacionesVideo

    }
}
