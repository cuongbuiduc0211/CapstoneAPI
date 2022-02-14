using AutoMapper;
using DatabaseAccess.Entities;
using DatabaseAccess.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Models;

namespace Services.Services
{
    public class GenerationAttributionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenerationAttributionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> CreateGenWithAtts(GenWithAttributions item)
        {
            try
            {
                foreach (var att in item.AttributionWithValues)
                {
                    GenerationAttribution newGenAtt = new GenerationAttribution()
                    {
                        Id = Guid.NewGuid().ToString(),
                        GenerationId = item.GenerationId,
                        AttributionId = att.AttributionId,
                        Value = att.Value
                    };
                    await _unitOfWork.GenerationAttributionRepository.Add(newGenAtt);
                    await _unitOfWork.SaveAsync();
                }

                return true;
            }
            catch(Exception ex)
            {
                //log(ex)
                return false;
            }
            
        }
        public async Task<IEnumerable<GenerationAttribution>> GetGenerationWithAtts(string generationId)
        {
            return await _unitOfWork.GenerationAttributionRepository.GetAll(
                q => q.GenerationId == generationId, 
                s => s.OrderBy(o => o.Attribution.Name), "Generation, Attribution");
        }

        public async Task<bool> UpdateGenWithAtts(GenWithAttributions item)
        {
            try
            {                               
                foreach (var att in item.AttributionWithValues)
                {
                    var itemInDB = await _unitOfWork.GenerationAttributionRepository.GetFirstOrDefault(
                        q => q.GenerationId == item.GenerationId && q.AttributionId == att.AttributionId);
                    if (itemInDB != null)
                    {
                        itemInDB.Value = att.Value;
                        _unitOfWork.GenerationAttributionRepository.Update(itemInDB);
                        await _unitOfWork.SaveAsync();
                    }
                    else
                    {
                        GenerationAttribution newGenAtt = new GenerationAttribution()
                        {
                            Id = Guid.NewGuid().ToString(),
                            GenerationId = item.GenerationId,
                            AttributionId = att.AttributionId,
                            Value = att.Value
                        };
                        await _unitOfWork.GenerationAttributionRepository.Add(newGenAtt);
                        await _unitOfWork.SaveAsync();
                    }
                    
                }
                return true;
            }
            catch(Exception ex)
            {
                //log(ex)
                return false;
            }
                   
        }
        public async Task<bool> RemoveGenWithAtts(string generationId)
        {
            List<GenerationAttribution> listGenAtts = (await _unitOfWork.GenerationAttributionRepository.GetAll(
                q => q.GenerationId == generationId)).ToList();
            if (listGenAtts != null)
            {
                _unitOfWork.GenerationAttributionRepository.RemoveRange(listGenAtts);
                await _unitOfWork.SaveAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
