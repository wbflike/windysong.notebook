﻿using System;
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
    }
}
