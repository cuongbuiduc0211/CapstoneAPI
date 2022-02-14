using AutoMapper;
using DatabaseAccess.Entities;
using DatabaseAccess.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Models;

namespace Services.Services
{
    public class AttributionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttributionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<ResponseAttribution>> CreateNewAttribution(List<AttributionItem> items)
        {
            //10 item, 3 trùng 7 ko trùng
            List<ResponseAttribution> listResAtts = new List<ResponseAttribution>();
            foreach (var item in items)
            {
                Attribution existedAtt = await _unitOfWork.AttributionRepository.GetFirstOrDefault(
                    q => q.Name.ToLower().Equals(item.Name.ToLower()));
                if (existedAtt != null)
                {
                    listResAtts.Add(new ResponseAttribution()
                    {
                        Name = item.Name,
                        result = false
                    });                   
                }
                else
                {
                    Attribution newAtt = _mapper.Map<Attribution>(item);
                    newAtt.Id = Guid.NewGuid().ToString();
                    newAtt.Type = (int)item.Type;
                    await _unitOfWork.AttributionRepository.Add(newAtt);
                    await _unitOfWork.SaveAsync();
                    listResAtts.Add(new ResponseAttribution()
                    {
                        Name = item.Name,
                        result = true
                    });
                }
            }
            return listResAtts;
        }

        public async Task<IEnumerable<Attribution>> GetAllAttsbByType(string typeId) 
        {
            return await _unitOfWork.AttributionRepository.GetAll(
                q => q.EngineType == typeId, o => o.OrderBy(s => s.Name));
        }

        public async Task<bool> UpdateAttribution(string id, AttributionItem attItem)
        {
            Attribution updated = await _unitOfWork.AttributionRepository.GetFirstOrDefault(
                q => q.Id == id);
            List<Attribution> existedList = (await _unitOfWork.AttributionRepository.GetAll(
                q => q.Id != id)).ToList();
            foreach (var item in existedList)
            {
                if (item.Name.ToLower().Equals(attItem.Name.ToLower()))
                {
                    return false;
                }
            }
            updated = _mapper.Map<AttributionItem, Attribution>(attItem);
            updated.Type = (int)attItem.Type;
            updated.Id = id;
            _unitOfWork.AttributionRepository.Update(updated);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveAttribution(string id)
        {
            Attribution attribution = await _unitOfWork.AttributionRepository.GetFirstOrDefault(q => q.Id == id);
            if (attribution != null)
            {
                _unitOfWork.AttributionRepository.Remove(attribution);
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
