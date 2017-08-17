using AutoMapper;
using Microsoft.Azure.Mobile.Server;
using spectro.camera.service.DataObjects;
using spectro.camera.service.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;

namespace spectro.camera.service.Models
{

    public class projectDomainManager : MappedEntityDomainManager<projectDTO, project>
    {
        private spectroCamContext context;

        public projectDomainManager(spectroCamContext context, HttpRequestMessage request)
            : base(context, request)
        {
            Request = request;
            this.context = context;
        }

        public static int GetKey(string projectDTOId, DbSet<project> projects, HttpRequestMessage request)
        {
            try
            {
                int projectId = projects
                   .Where(c => c.Id == projectDTOId)
                   .Select(c => c.ProjectId)
                   .FirstOrDefault();

                if (projectId == 0)
                {
                    throw new HttpResponseException(request.CreateNotFoundResponse());
                }
                return projectId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override T GetKey<T>(string projectDTOId)
        {
            try
            {
                return (T)(object)GetKey(projectDTOId, this.context.projects, this.Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override SingleResult<projectDTO> Lookup(string projectDTOId)
        {
            try
            {
                int projectId = GetKey<int>(projectDTOId);
                return LookupEntity(c => c.ProjectId == projectId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public override async Task<projectDTO> InsertAsync(projectDTO entityDTO)
        {

            return await base.InsertAsync(entityDTO);
        }

        public override async Task<projectDTO> UpdateAsync(string entityDTOId, Delta<projectDTO> patch)
        {


            int Id = GetKey<int>(entityDTOId);

            var existingproject = await this.Context.Set<project>().FindAsync(Id);
            if (existingproject == null)
            {
                throw new HttpResponseException(this.Request.CreateNotFoundResponse());
            }

            projectDTO existingprojectDTO = Mapper.Map<project, projectDTO>(existingproject);
            patch.Patch(existingprojectDTO);
            Mapper.Map<projectDTO, project>(existingprojectDTO, existingproject);



            await this.SubmitChangesAsync();

            projectDTO updatedprojectDTO = Mapper.Map<project, projectDTO>(existingproject);

            return updatedprojectDTO;
        }

        public override async Task<projectDTO> ReplaceAsync(string entityDTOId, projectDTO entityDTO)
        {

            return await base.ReplaceAsync(entityDTOId, entityDTO);
        }


        public override async Task<bool> DeleteAsync(string projectDTOId)
        {
            try
            {
                int projectId = GetKey<int>(projectDTOId);
                return await DeleteItemAsync(projectId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class spectraDomainManager : MappedEntityDomainManager<spectraDTO, spectra>
    {
        private spectroCamContext context;

        public spectraDomainManager(spectroCamContext context, HttpRequestMessage request)
            : base(context, request)
        {
            Request = request;
            this.context = context;
        }

        public static int GetKey(string spectraDTOId, DbSet<spectra> spectras, HttpRequestMessage request)
        {
            try
            {
                int spectraId = spectras
                   .Where(c => c.Id == spectraDTOId)
                   .Select(c => c.SpectraId)
                   .FirstOrDefault();

                if (spectraId == 0)
                {
                    throw new HttpResponseException(request.CreateNotFoundResponse());
                }
                return spectraId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override T GetKey<T>(string spectraDTOId)
        {
            try
            {
                return (T)(object)GetKey(spectraDTOId, this.context.spectras, this.Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override SingleResult<spectraDTO> Lookup(string spectraDTOId)
        {
            try
            {
                int spectraId = GetKey<int>(spectraDTOId);
                return LookupEntity(c => c.SpectraId == spectraId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private async Task<project> VerifyprojectDTO(string Id)
        {
            int _Id = projectDomainManager.GetKey(Id, this.context.projects, this.Request);
            project _project = await this.context.projects.FindAsync(_Id);
            if (_project == null)
            {
                throw new HttpResponseException(Request.CreateBadRequestResponse("project with id '{0}' was not found", _Id));
            }
            return _project;
        }



        public override async Task<spectraDTO> InsertAsync(spectraDTO entityDTO)
        {

            var ProjectIdModel = await VerifyprojectDTO(entityDTO.ProjectIdDTO);
            entityDTO.ProjectId = ProjectIdModel.ProjectId;

            return await base.InsertAsync(entityDTO);
        }

        public override async Task<spectraDTO> UpdateAsync(string entityDTOId, Delta<spectraDTO> patch)
        {

            var ProjectIdModel = await VerifyprojectDTO(patch.GetEntity().ProjectIdDTO);


            int Id = GetKey<int>(entityDTOId);

            var existingspectra = await this.Context.Set<spectra>().FindAsync(Id);
            if (existingspectra == null)
            {
                throw new HttpResponseException(this.Request.CreateNotFoundResponse());
            }

            spectraDTO existingspectraDTO = Mapper.Map<spectra, spectraDTO>(existingspectra);
            patch.Patch(existingspectraDTO);
            Mapper.Map<spectraDTO, spectra>(existingspectraDTO, existingspectra);


            // This is required to map the right Id for the projects
            existingspectra.ProjectId = ProjectIdModel.ProjectId;


            await this.SubmitChangesAsync();

            spectraDTO updatedspectraDTO = Mapper.Map<spectra, spectraDTO>(existingspectra);

            return updatedspectraDTO;
        }

        public override async Task<spectraDTO> ReplaceAsync(string entityDTOId, spectraDTO entityDTO)
        {
            await VerifyprojectDTO(entityDTO.ProjectIdDTO);

            return await base.ReplaceAsync(entityDTOId, entityDTO);
        }


        public override async Task<bool> DeleteAsync(string spectraDTOId)
        {
            try
            {
                int spectraId = GetKey<int>(spectraDTOId);
                return await DeleteItemAsync(spectraId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}