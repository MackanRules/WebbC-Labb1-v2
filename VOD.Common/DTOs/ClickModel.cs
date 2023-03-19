using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Common.DTOs
{
	public record ClickModel(string PageType, int Id);
	public record RefClickModel<TRefDTO>(string pageType, TRefDTO dto);

}