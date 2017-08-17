using Microsoft.Azure.Mobile.Server;
using spectro.camera.service.DataObjects;
using spectro.camera.service.Models; 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Web.Http.OData;

namespace spectro.camera.service.Controllers
{

    //[Authorize]
    public class projectController : TableController<projectDTO>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            spectroCamContext context = new spectroCamContext();
            DomainManager = new projectDomainManager(context, Request);
        }

        public IQueryable<projectDTO> GetAllprojectDTOs()
        {
            try
            {
                return Query();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SingleResult<projectDTO> GetprojectDTO(string id)
        {
            try
            {
                return Lookup(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<projectDTO> PatchprojectDTO(string id, Delta<projectDTO> patch)
        {
            try
            {

                return UpdateAsync(id, patch);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ResponseType(typeof(projectDTO))]
        public async Task<IHttpActionResult> PostprojectDTO(projectDTO item)
        {
            try
            {
                projectDTO current = await InsertAsync(item);
                return CreatedAtRoute("Tables", new { id = current.Id }, current);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task DeleteprojectDTO(string id)
        {
            try
            {
                return DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    //[Authorize]
    public class spectraController : TableController<spectraDTO>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            spectroCamContext context = new spectroCamContext();
            DomainManager = new spectraDomainManager(context, Request);
        }

        public IQueryable<spectraDTO> GetAllspectraDTOs()
        {
            try
            {
                return Query();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SingleResult<spectraDTO> GetspectraDTO(string id)
        {
            try
            {
                return Lookup(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<spectraDTO> PatchspectraDTO(string id, Delta<spectraDTO> patch)
        {
            try
            {

                return UpdateAsync(id, patch);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [ResponseType(typeof(spectraDTO))]
        public async Task<IHttpActionResult> PostspectraDTO(spectraDTO item)
        {
            try
            {
                spectraDTO current = await InsertAsync(item);
                return CreatedAtRoute("Tables", new { id = current.Id }, current);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task DeletespectraDTO(string id)
        {
            try
            {
                return DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }



}