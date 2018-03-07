using System;
using System.Collections.Generic;
using System.Text;
using WindySong.NoteBook.App.ViewModels.Admin;
using WindySong.NoteBook.App.ViewModels.Json;

namespace WindySong.NoteBook.App.Interfaces
{
    /// <summary>
    /// 笔记操作接口
    /// </summary>
    public interface INoteBookAppService
    {
        /// <summary>
        /// 获取TAB分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        JsonPagTab GetPageTab(DataPageModel model);

        /// <summary>
        /// 添加TAB
        /// </summary>
        /// <param name="model">TabAddModel</param>
        /// <returns></returns>
        bool AddTab(TabModel model);

        /// <summary>
        /// 更新TAB
        /// </summary>
        /// <param name="model">UpdateAddModel</param>
        /// <returns></returns>
        bool UpdateTab(TabModel model);

        /// <summary>
        /// 删除TAB
        /// </summary>
        /// <param name="model">DeleteModel</param>
        /// <returns></returns>
        int DeleteTab(DeleteModel model);

        /// <summary>
        /// 获取Tab Select
        /// </summary>
        /// <returns></returns>
        JsonSelect GetTabSelect();

        /// <summary>
        /// 添加Col
        /// </summary>
        /// <param name="model">ColModel</param>
        /// <returns></returns>
        bool AddCol(ColModel model);

        /// <summary>
        /// 获取Col分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        JsonPagCol GetPageCol(DataPageModel model);

        /// <summary>
        /// 更新Col
        /// </summary>
        /// <param name="model">ColModel</param>
        /// <returns></returns>
        bool UpdateCol(ColModel model);

        /// <summary>
        /// 删除Col
        /// </summary>
        /// <param name="model">DeleteModel</param>
        /// <returns></returns>
        int DeleteCol(DeleteModel model);

        /// <summary>
        /// 获取Col Select
        /// </summary>
        /// <returns></returns>
        JsonSelect GetColSelect();

        /// <summary>
        /// 添加Block
        /// </summary>
        /// <param name="model">BlockModel</param>
        /// <returns></returns>
        bool AddBlock(BlockModel model);

        /// <summary>
        /// 获取Block分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        JsonPagBlock GetPageBlock(DataPageModel model);

        /// <summary>
        /// 更新Block
        /// </summary>
        /// <param name="model">BlockModel</param>
        /// <returns></returns>
        bool UpdateBlock(BlockModel model);

        /// <summary>
        /// 删除Block
        /// </summary>
        /// <param name="model">DeleteModel</param>
        /// <returns></returns>
        int DeleteBlock(DeleteModel model);

        /// <summary>
        /// 获取Block Select
        /// </summary>
        /// <returns></returns>
        JsonSelect GetBlockSelect();

        /// <summary>
        /// 获取Block Select
        /// </summary>
        /// <param name="id">TabId</param>
        /// <returns></returns>
        JsonSelect GetBlockSelect(int id);

        /// <summary>
        /// 添加List
        /// </summary>
        /// <param name="model">ListModel</param>
        /// <returns></returns>
        bool AddList(ListModel model);

        /// <summary>
        /// 获取List分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        JsonPagList GetPageList(DataPageModel model);

        /// <summary>
        /// 更新List
        /// </summary>
        /// <param name="model">ListModel</param>
        /// <returns></returns>
        bool UpdateList(ListModel model);

        /// <summary>
        /// 删除List
        /// </summary>
        /// <param name="model">DeleteModel</param>
        /// <returns></returns>
        int DeleteList(DeleteModel model);

        /// <summary>
        /// 获取List Select
        /// </summary>
        /// <param name="id">BlockId</param>
        /// <returns></returns>
        JsonSelect GetListSelect(int id);

        /// <summary>
        /// 添加Api
        /// </summary>
        /// <param name="model">ApiModel</param>
        /// <returns></returns>
        bool AddApi(ApiModel model);

        /// <summary>
        /// 获取Api分页数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        JsonPagApi GetPageApi(DataPageModel model);

        /// <summary>
        /// 获取Api
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        ApiModel GetApi(int id);

        /// <summary>
        /// 更新Api
        /// </summary>
        /// <param name="model">ApiModel</param>
        /// <returns></returns>
        bool UpdateApi(ApiModel model);

        /// <summary>
        /// 删除Api
        /// </summary>
        /// <param name="model">DeleteModel</param>
        /// <returns></returns>
        int DeleteApi(DeleteModel model);
    }
}
