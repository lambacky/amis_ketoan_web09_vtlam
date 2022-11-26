<template>
    <BaseTable :headers="headers" :dataList="employees" :mapData="mapData" 
    @modifyForm="modifyForm" @selectDuplicate="selectDuplicate" @selectDelete="selectDelete"
    @checkAll="checkAll" @checkOne="checkOne"/>
</template>

<script>
import BaseTable from '../../components/base/BaseTable.vue'
import  FormMode  from "@/enums/formMode.js"
import AlertAction from "../../enums/alertAction.js"
import { mapActions, mapGetters } from "vuex"
import resourceVN from "../../resources/resourceVN"
export default {
    name:"EmployeeTable",
    components:{BaseTable},
    computed: mapGetters([
        "employees",
        "totalEmployee",
        "singleEmployee",
        "checkedEmployeeIds",
    ]),
    methods: {
        ...mapActions([
            "changeFormMode",
            "toggleDialog",
            "toggleAlert",
            "setCheckedEmployeeIds",
            "getEmployee",
            "selectEmployee",
            "setAlert",
            "setDialogTitle",
        ]),

        /**
         * format lại ngày tháng
         * @param {date} dob
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        formatDate(dob){
            if (dob) {
                dob = new Date(dob);
                // Lấy ngày
                let date = dob.getDate();
                date = date < 10 ? `0${date}` : date;
                // Lấy tháng
                let month = dob.getMonth() + 1;
                month = month < 10 ? `0${month}` : month;
                // Lấy năm
                let year = dob.getFullYear();

                dob = `${date}/${month}/${year}`;
            }
            return dob;
        },

        /**
         * chỉnh sửa nhân viên
         * @param {*} emp
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        modifyForm(emp){
            const me = this;
            me.setDialogTitle(resourceVN.Text.modifyFormTitle);
            me.changeFormMode(FormMode.EDIT);
            me.selectEmployee(emp);
            me.toggleDialog();
        },

        /**
         * chọn xóa nhân viên
         * @param {*} emp
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        selectDelete(emp){
            const me = this;
            me.selectEmployee(emp);
            me.setAlert({
                type:"warning",
                message: resourceVN.AlertMessage.deleteWarning.replace("0",me.singleEmployee.EmployeeCode),
                action: AlertAction.CONFIRM_DELETE
            });

        },

        /**
         * chọn nhân bản
         * @param {*} emp
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        selectDuplicate(emp){
            const me = this;
            me.setDialogTitle(resourceVN.Text.addFormTitle);
            me.changeFormMode(FormMode.STORE);
            me.selectEmployee(emp);
            me.toggleDialog();
        },

        /**
         * check tất cả checkbox
         * @param {*} event
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        checkAll(event){
            const me=this;
            for(const emp of me.employees){
                if(event.target.checked==true){
                    if(!me.checkedEmployeeIds.includes(emp.EmployeeId)){
                        me.setCheckedEmployeeIds(emp.EmployeeId);
                    }
                }else{
                    me.setCheckedEmployeeIds(emp.EmployeeId);
                }
            }
        },

        /**
         * check từng checkbox
         * @param {*} emp
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        checkOne(emp){
            const me=this;
            me.setCheckedEmployeeIds(emp.EmployeeId);
        },

        /**
         * lấy dữ liệu cần thiết từ nhân viên
         * @param {*} emp
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        mapData(emp){
            let me=this;
            var arr=[
                emp.EmployeeCode,
                emp.EmployeeName,
                emp.GenderName,
                me.formatDate(emp.DateOfBirth),
                emp.IdentityNumber,
                emp.EmployeePosition,
                emp.DepartmentName,
                emp.BankAccountNumber,
                emp.BankName,
                emp.BankBranchName
                ];
            return arr;
        }
    },
    data() {
        return {
            headers:[
                {
                    name:resourceVN.FieldName.employeeCode.toUpperCase(),
                    minWidth:"140px"
                },
                {
                    name:resourceVN.FieldName.employeeName.toUpperCase(),
                    minWidth:"170px"
                },
                {
                    name:resourceVN.FieldName.gender.toUpperCase(),
                    minWidth:"105px"
                },
                {
                    name:resourceVN.FieldName.dateOfBirth.toUpperCase(),
                    minWidth:"120px",
                    class:"date"
                },
                {
                    name:resourceVN.FieldName.identityNumber.toUpperCase(),
                    minWidth:"150px",
                    title:resourceVN.FieldName.identityNumberToolTip
                },
                {
                    name:resourceVN.FieldName.employeePosition.toUpperCase(),
                    minWidth:"120px"
                },
                {
                    name:resourceVN.FieldName.departmentName.toUpperCase(),
                    minWidth:"230px"
                },
                {
                    name:resourceVN.FieldName.bankAccountNumber.toUpperCase(),
                    minWidth:"150px"
                },
                {
                    name:resourceVN.FieldName.bankName.toUpperCase(),
                    minWidth:"160px"
                },
                {
                    name:resourceVN.FieldName.bankBranchName.toUpperCase(),
                    minWidth:"240px",
                    title:resourceVN.FieldName.bankBranchNameToolTip
                },
            ]
        }
    },
}
</script>