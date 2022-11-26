import FormMode from "@/enums/formMode";
import Gender from "@/enums/gender";
import AlertAction from "@/enums/alertAction";
import CONST_API from "@/constants/api.js";
import axios from "axios";
import state from "./state"
import resourceVN from "@/resources/resourceVN";
const actions = {
    /**
     * Đặt tiêu đề cho form nhân viên
     * @param {*} context 
     * @param {*} title 
     */
    setDialogTitle(context,title){
        context.commit("setDialogTitle", title);
    },

    /**
     * đổi chế độ của form
     * @param {*} context 
     * @param {*} mode
     * Author:Vũ Tùng Lâm (30/10/2022)
     */
    changeFormMode(context, mode) {
        context.commit("changeFormMode", mode);
    },

    /**
     * Ẩn/hiện form dialog
     * @param {*} context 
     * Author:Vũ Tùng Lâm (30/10/2022)
     */
    toggleDialog(context) {
        context.commit("toggleDialog");
    },

    /**
     * lấy danh sách những nhân viên được chọn
     * @param {*} context 
     * @param {*} id 
     * Author:Vũ Tùng Lâm (30/10/2022)
     */
    setCheckedEmployeeIds(context,id){
        context.commit("setCheckedEmployeeIds",id);
    },


    /**
     * Xử lí bộ lọc danh sách nhân viên
     * @param {*} context
     * @param {*} filter
     * Author:Vũ Tùng Lâm (30/10/2022)
     */
    setFilter(context,filter){
        context.commit("setFilter",filter);
    },
    

    /**
     * Lấy dữ liệu nhân viên
     * @param {*} context 
     * Author:Vũ Tùng Lâm (30/10/2022)
     */
    async getEmployee(context){
        try {
            context.dispatch("toggleLoading");
            const res = await axios.get(
            `${CONST_API}/Employees/filter`, {params:{pageSize: state.filter.pageSize, pageNumber:state.filter.pageNumber,employeeFilter:state.filter.employeeFilter}}
            );
            document.querySelectorAll("table input").forEach(checkbox => checkbox.checked=false); 
            context.commit("getEmployee", res.data);
        } catch (error) {
            console.error(error);
        } finally{
            context.dispatch("toggleLoading");
        }
    },

    /**
     * Lấy mã nhân viên mới
     * @param {*} context 
     * Author:Vũ Tùng Lâm (30/10/2022)
     */
    async setNewEmployeeCode(context) {
        try {
          const res = await axios.get(
            `${CONST_API}/Employees/NewEmployeeCode`
          );
          context.commit("setNewEmployeeCode", res.data);
        } catch (error) {
          console.log(error);
        }
    },

    /**
     * Thêm nhân viên
     * @param {*} context 
     * Author:Vũ Tùng Lâm (30/10/2022)
     */
    async addEmployee(context) {
        try {
            await axios.post(`${CONST_API}/Employees`, state.employee);
            //thông báo thành công
            context.dispatch("setToastMessage", {
                type: "success",
                message: resourceVN.AlertMessage.addEmployeeSuccess,
            });
        
            // Check mode
            if (state.formMode == FormMode.STORE) {
                // Cất
                context.dispatch("toggleDialog");
            } else {
                // Cất và thêm
                context.dispatch("changeFormMode", FormMode.STORE);
                context.dispatch("selectEmployee", { Gender: Gender.MALE });
                context.dispatch("setNewEmployeeCode");
            }

            //load lại dữ liệu
            context.dispatch("getEmployee");
        } catch (error) {
            handleException(error, context);
        }
    },

    /**
     * Chỉnh sửa nhân viên
     * @param {*} context 
     * Author:Vũ Tùng Lâm (30/10/2022)
     */
    async editEmployee(context) {
        try {
            await axios.put(
                `${CONST_API}/Employees/${state.employee.EmployeeId}`,state.employee);
            
            //thông báo thành công
            context.dispatch("setToastMessage", {
                type: "success",
                message: resourceVN.AlertMessage.editEmployeeSuccess,
            });
        
            // Check mode
            if (state.formMode == FormMode.EDIT) {
                // Cất
                context.dispatch("toggleDialog");
            } else {
                // Cất và thêm
                context.dispatch("setDialogTitle","Thêm khách hàng");
                context.dispatch("changeFormMode", FormMode.STORE);
                context.dispatch("selectEmployee", { Gender: Gender.MALE });
                context.dispatch("setNewEmployeeCode");
            }

            //load lại dữ liệu
            context.dispatch("getEmployee");
        } catch (error) {
            handleException(error, context);
        }
    },

    /**
     * Xóa nhân viên
     * @param {*} context 
     * @param {string} id
     * Author:Vũ Tùng Lâm (30/10/2022)
     */
    async deleteEmployee(context){
        try {
            await axios.delete(`${CONST_API}/Employees/${state.employee.EmployeeId}`);

            //thông báo thành công
            context.dispatch("setToastMessage", {
                type: "success",
                message: resourceVN.AlertMessage.deleteEmployeeSuccess,
            });

            //Quay về trang đầu tiên nếu xóa hết bản ghi ở trang cuối cùng
            if(state.filter.pageNumber == state.totalPage && state.employees.length==1){
                context.dispatch("setFilter",{
                    pageSize:state.filter.pageSize,
                    pageNumber: 1,
                    employeeFilter: state.filter.employeeFilter
                })
            }
            //load lại dữ liệu
            context.dispatch("getEmployee");
        } catch (error) {
            handleException(error, context);
        }
    },

    /**
     * Xóa hàng loạt nhân viên
     * @param {*} context 
     * @param {string} id
     * Author:Vũ Tùng Lâm (30/10/2022)
     */
     async deleteBatchEmployee(context){
        try {
            
            await axios.post(`${CONST_API}/Employees/deleteBatch`,state.checkedEmployeeIds);
            //thông báo thành công
            context.dispatch("setToastMessage", {
                type: "success",
                message: resourceVN.AlertMessage.deleteEmployeeSuccess,
            });

            //Quay về trang đầu tiên nếu xóa hết bản ghi ở trang cuối cùng
            if(state.filter.pageNumber == state.totalPage && state.employees.length==state.checkedEmployeeIds.length){
                context.dispatch("setFilter",{
                    pageSize:state.filter.pageSize,
                    pageNumber: 1,
                    employeeFilter: state.filter.employeeFilter
                })
            }
            //load lại dữ liệu
            context.dispatch("getEmployee");
        } catch (error) {
            handleException(error, context);
        }
    },

    /**
     * Chọn nhân viên
     * @param {*} context 
     * @param {*} emp
     * Author:Vũ Tùng Lâm (30/10/2022)
     */
    selectEmployee(context,emp){
        context.commit("selectEmployee",emp);
    },

    /**
     * xuất dữ liệu ra file excel
     * @param {*} context 
     * Author:Vũ Tùng Lâm (30/10/2022)
     */
    exportToExcel(context) {
        axios({
              url: `${CONST_API}/Employees/exportToExcel`,
              method: 'GET',
              responseType: 'blob',
        }).then((res) => {
               var FILE = window.URL.createObjectURL(new Blob([res.data]));
               var docUrl = document.createElement('a');
               docUrl.href = FILE;
               docUrl.setAttribute('download', 'Danh_sach_nhan_vien.xlsx');
               document.body.appendChild(docUrl);
               docUrl.click();
        }).catch((error) => handleException(error, context));
    }

}
/**
 * Xử lí lỗi
 * @param {*} error 
 * @param {*} context 
 */
const handleException = (error, context) => {
    //thông báo có lỗi
    context.dispatch("setAlert", {
      type: "danger",
      message: error.response.data.UserMsg,
      action: AlertAction.DEFAULT,
    });
};

export default actions