<template>
    <div id="formDialog" class="dialog-wrapper">
        <div class="dialog dialog-add-emp">
            <div class="dialog-header">
                <div class="dialog-header-left">
                    <div class="dialog-title">{{dialogTitle}}</div>
                    <div class="input-checkbox-wrapper">
                        <input ref="First" tabindex="1"  class="input-checkbox" type="checkbox">
                        <label class="input-checkbox-label">{{Text.isCustomer}}</label>
                    </div>
                    <div class="input-checkbox-wrapper">
                        <input ref="Last" tabindex="21"  class="input-checkbox" type="checkbox">
                        <label class="input-checkbox-label">{{Text.isProvider}}</label>
                    </div>
                </div>
                <div class="dialog-header-right">
                    <div class="icon icon-help" :title="Text.helpToolTip"></div>
                    <div @click="escDialog" id="btnEsc" class="icon icon-close" :title="Text.escToolTip"></div>
                </div>
            </div>
            <form id="employeeForm" class="dialog-body">
                <div class="dialog-body-top">
                    <div class="dialog-body-left w-50">
                        <div class="row-flex">
                            <BaseInput @keydown="focusLast" ref="EmployeeCode" :inputLabel="FieldName.employeeCode" :inputWidth="'w-40'" :validateType="'required'" v-model="employee.EmployeeCode" :errorMess="errors.EmployeeCode" :tabIndex="2" />
                            <BaseInput :inputLabel="FieldName.employeeName" :inputWidth="'flex-1'" :validateType="'required'" v-model="employee.EmployeeName" :errorMess="errors.EmployeeName" :tabIndex="3"  />
                        </div>
                        <div class="input-wrapper" style="overflow:visible">
                            <label class="input-label">{{FieldName.departmentName}}<span class="require-mark">*</span></label>
                            <BaseCombobox :errorMess="errors.DepartmentName" :dataList="departments.map(dep => dep.DepartmentName)" :className="'department-list'" 
                                            :selectedItem="employee.DepartmentName" @selectAction="selectDepartment" :tabIndex="6"/>
                        </div>
                        <BaseInput :inputLabel="FieldName.employeePosition" v-model="employee.EmployeePosition" :errorMess="errors.EmployeePosition" :tabIndex="9" />
                    </div>
                    <div class="dialog-body-right w-50">
                        <div class="row-flex">
                            <BaseInput :inputLabel="'Ngày sinh'" :inputType="'date'" :inputWidth="'w-40'" v-model="employee.DateOfBirth" :errorMess="errors.DateOfBirth" :tabIndex="4" />
                            <div class="input-wrapper flex-1">
                                <label class="input-label">{{FieldName.gender}}</label>
                                <div class="input-checkbox-list gender-list">
                                    <div class="input-checkbox-wrapper">
                                        <input class="input-checkbox" type="radio" value="0" name="Gender" :checked="employee.Gender === 0" tabindex="5" @change="(e)=>(employee.Gender = parseInt(e.target.value))">
                                        <label class="input-label">{{Text.male}}</label>
                                    </div>
                                    <div class="input-checkbox-wrapper">
                                        <input class="input-checkbox" type="radio" value="1" name="Gender" :checked="employee.Gender === 1" tabindex="5" @change="(e)=>(employee.Gender = parseInt(e.target.value))">
                                        <label class="input-label">{{Text.female}}</label>
                                    </div>
                                    <div class="input-checkbox-wrapper">
                                        <input class="input-checkbox" type="radio" value="2" name="Gender" :checked="employee.Gender === 2" tabindex="5" @change="(e)=>(employee.Gender = parseInt(e.target.value))">
                                        <label class="input-label">{{Text.other}}</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row-flex">
                            <BaseInput :inputLabel="FieldName.identityNumber" :inputWidth="'w-60'" v-model="employee.IdentityNumber" :errorMess="errors.IdentityNumber" :tabIndex="7" />
                            <BaseInput :inputLabel="FieldName.identityDate" :inputType="'date'" :inputWidth="'flex-1'" v-model="employee.IdentityDate" :errorMess="errors.IdentityDate" :tabIndex="8" />
                        </div>
                        <BaseInput :inputLabel="FieldName.identityPlace" v-model="employee.IdentityPlace" :errorMess="errors.IdentityPlace" :tabIndex="10" />
                </div>
                
                </div>
                <div class="dialog-body-bottom">
                    <BaseInput :inputLabel="FieldName.address" v-model="employee.Address" :errorMess="errors.Address" :tabIndex="11" />
                    <div class="row-flex">
                        <BaseInput :inputLabel="FieldName.telephoneNumber" :labelTooltip="FieldName.telephoneNumberToolTip" :inputWidth="'w-25'" v-model="employee.TelephoneNumber" :errorMess="errors.TelephoneNumber" :tabIndex="12" />
                        <BaseInput :inputLabel="FieldName.phoneNumber" :labelTooltip="FieldName.phoneNumberToolTip" :inputWidth="'w-25'" v-model="employee.PhoneNumber" :errorMess="errors.PhoneNumber" :tabIndex="13" />
                        <BaseInput :inputLabel="FieldName.email" :inputWidth="'w-25'" v-model="employee.Email" :errorMess="errors.Email" :tabIndex="14" />
                        <div class="w-25"></div>
                    </div>
                    <div class="row-flex">
                        <BaseInput :inputLabel="FieldName.bankAccountNumber" :inputWidth="'w-25'" v-model="employee.BankAccountNumber" :errorMess="errors.BankAccountNumber" :tabIndex="15" />
                        <BaseInput :inputLabel="FieldName.bankName" :inputWidth="'w-25'" v-model="employee.BankName" :errorMess="errors.BankName" :tabIndex="16" />
                        <BaseInput :inputLabel="FieldName.bankBranchName" :labelTooltip="FieldName.bankBranchNameToolTip" :inputWidth="'w-25'" v-model="employee.BankBranchName" :errorMess="errors.BankBranchName" :tabIndex="17" />
                        <div class="w-25"></div>
                    </div>
                </div>
            </form>
            <div class="dialog-footer">
                <BaseButton :btnText="Text.cancel" :isSecondary="true" @click="toggleDialog" @keydown="focusFirst" tabIndex="20"/>
                <div class="btn-action">
                    <BaseButton :btnText="Text.store" :isSecondary="true" @click="saveData" tabIndex="19"/>
                    <BaseButton :btnText="Text.storeAdd" @click="saveAddData" tabIndex="18"/>

                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { mapActions, mapGetters } from "vuex"
import FormMode from "../../enums/formMode.js"
import AlertAction from "../../enums/alertAction.js"
import BaseButton from "../../components/base/BaseButton.vue"
import BaseCombobox from "../../components/base/BaseCombobox.vue"
import BaseInput from "../../components/base/BaseInput.vue"
import resourceVN from "../../resources/resourceVN"

export default {
    name:"EmployeeDialog",
    components:{BaseButton,BaseInput,BaseCombobox},
    computed:mapGetters([
        "isShowDialog",
        "formMode",
        "employee",
        "singleEmployee",
        "departments",
        "alert",
        "dialogTitle",
    ]),
    created() {
        const me=this;
        //lấy mã nhân viên mới
        if(me.formMode == FormMode.STORE){
            me.setNewEmployeeCode();
        }
        
    },
    mounted(){
        let me=this;
        //focus vào input mã nhân viên
        me.$refs.EmployeeCode.$el.querySelector("input").focus();
        document.addEventListener("keydown",me.shortcutKey);
    },
    unmounted(){
        let me=this;
        document.removeEventListener("keydown",me.shortcutKey);
    },
    props: ["isStore"],
    emits: ["isStoreDone"],
    methods: {
        ...mapActions([
            "changeFormMode",
            "toggleDialog",
            "toggleAlert",
            "setNewEmployeeCode",
            "setAlert",
            "addEmployee",
            "editEmployee",
        ]),
        
        /**
         * chọn nút cất
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        saveData(){
            const me=this;
            if(me.formMode==FormMode.STORE_AND_ADD){
                me.changeFormMode(FormMode.STORE);
            }
            if(me.formMode==FormMode.EDIT_AND_ADD){
                me.changeFormMode(FormMode.EDIT);
            }
            me.storeEmployee();
        },

        /**
         * chọn nút cất và thêm
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        saveAddData(){
            const me=this;
            if(me.formMode==FormMode.STORE){
                me.changeFormMode(FormMode.STORE_AND_ADD);
            }
            if(me.formMode==FormMode.EDIT){
                me.changeFormMode(FormMode.EDIT_AND_ADD);
            }
            me.storeEmployee();
        },

        /**
         * cất dữ liệu nhân viên
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        storeEmployee(){
            const me=this;
            //validate dữ liệu
            let isValid = me.validateData();
            if(me.formMode==FormMode.STORE || me.formMode==FormMode.STORE_AND_ADD){
                me.employee.CreatedDate = (new Date()).toISOString();
                me.employee.ModifiedDate = (new Date()).toISOString();
            }
            if(me.formMode==FormMode.STORE || me.formMode==FormMode.STORE_AND_ADD){
                me.employee.ModifiedDate = (new Date()).toISOString();
            }
            me.employee.CreatedBy="Vũ Tùng Lâm";
            me.employee.ModifiedBy="Vũ Tùng Lâm";
            // me.employee.Gender = parseInt(document.querySelector('input[name="Gender"]:checked').value);
            
            if (isValid) {
                if (me.formMode == FormMode.STORE ||me.formMode == FormMode.STORE_AND_ADD) {
                    //thêm nhân viên
                    me.addEmployee();
                } else if (me.formMode == FormMode.EDIT ||me.formMode == FormMode.EDIT_AND_ADD) {
                    //sửa nhân viên
                    me.editEmployee();
                }
            }
        },
        
        /**
         * validate dữ liệu
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        validateData(){
            const me=this;
            var isValid = false;
            for (const prop of Object.getOwnPropertyNames(me.errors)) {
                delete me.errors[prop];
            }

            // Mã không được để trống
            if(!me.employee.EmployeeCode){
                me.errors.EmployeeCode=resourceVN.ErrorMessage.emptyEmployeeCode;
            }else{
                if(!me.employee.EmployeeCode.match(/(NV)(\d+)/)){
                    me.errors.EmployeeCode=resourceVN.ErrorMessage.invalidEmployeeCode;
                }
            }

            // Tên không được để trống
            if(!me.employee.EmployeeName){
                me.errors.EmployeeName=resourceVN.ErrorMessage.emptyEmployeeName;
                
            }

            // Đơn vị không được để trống
            if(!me.employee.DepartmentName){
                me.errors.DepartmentName=resourceVN.ErrorMessage.emptyDepartmentName;
                
            }

            //chuyển empty thành null
            if(me.employee.DateOfBirth==""){
                me.employee.DateOfBirth=null;
            }

            // Ngày sinh không được lớn hơn hiện tại
            if(me.employee.DateOfBirth){
                let currentDate = (new Date()).toISOString().split('T')[0];
                if(me.employee.DateOfBirth>currentDate){
                    me.errors.DateOfBirth=resourceVN.ErrorMessage.invalidDateOfBirth;
                    
                }
            }

            //chuyển empty thành null
            if(me.employee.DateOfBirth==""){
                me.employee.DateOfBirth=null;
            }

            // Ngày cấp không được lớn hơn hiện tại
            if(me.employee.IdentityDate){
                let currentDate = (new Date()).toISOString().split('T')[0];
                if(me.employee.IdentityDate>currentDate){
                    me.errors.IdentityDate=resourceVN.ErrorMessage.invalidIdentityDate;
                    
                }
            }

            // Email không được lớn hơn hiện tại
            if(me.employee.Email){
                var mailFormat = /^([a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+)@([a-zA-Z0-9-]+)((\.[a-zA-Z0-9-]{2,3})+)$/;
                if(!me.employee.Email.match(mailFormat)){
                    me.errors.Email=resourceVN.ErrorMessage.invalidEmail;
                }
            }

            // Kiểm tra xem có lỗi nào không
            if(Object.keys(me.errors).length == 0){
                isValid=true;
            }else{
                me.setAlert({
                    type:"danger",
                    message: Object.values(me.errors)[0],
                })
            }
            return isValid;           
        },

        /**
         * combobox chọn phòng ban
         * @param {*} dep
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        selectDepartment(departmentName){
            const me=this;
            // me.employee.DepartmentId = dep.DepartmentId;
            me.employee.DepartmentName = departmentName;
            for(const dep of me.departments){
                if(dep.DepartmentName==departmentName){
                    me.employee.DepartmentId=dep.DepartmentId;
                }
            }
        },

        /**
         * chọn nút thoát (X) khỏi form
         * @param {*} dep
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        escDialog() {
            const me=this;
            // Check dữ liệu trên form đã thay đổi
            if (JSON.stringify(me.employee) !== JSON.stringify(me.singleEmployee)) {
                me.setAlert({
                    type: "question",
                    message: resourceVN.AlertMessage.changeDataQuestion,
                    action: AlertAction.CONFIRM_STORE,
                });
            } else {
                me.toggleDialog();
            }
        },

        /**
         * focus từ nút cuối cùng quay về input đầu tiên
         * @param {*} e
         * Author: Vũ Tùng Lâm (9/11/2022)
         */
        focusFirst(e){
            if(!e.shiftKey && e.which==9){
                this.$refs.First.focus();
            }
            
        },
        /**
         * focus từ input đầu tiên về nút cuối cùng
         * @param {*} e
         * Author: Vũ Tùng Lâm (9/11/2022)
         */
        focusLast(e){
            if((e.shiftKey && e.which==9)){
                this.$refs.Last.focus();
            }
            
        },

        /**
         * xử lí phím tắt
         * @param {*} e
         * Author: Vũ Tùng Lâm (9/11/2022)
         */
        shortcutKey(e){
            let me=this;
            if(e.which==27){
                me.escDialog();
            }
            if(e.ctrlKey && e.key=="F8"){
                me.saveData();
            }
            if(e.ctrlKey && e.key=="F9"){
                me.toggleDialog();
            }
        }
        
    },
    watch: {
        isStore(newState) {
            const me=this;
            if (newState == true) {
                me.saveData();
                me.$emit("isStoreDone");
            }
        },
    },
    data() {
        return {
            errors:{},
            FieldName: resourceVN.FieldName,
            Text: resourceVN.Text
        }
    },
}
</script>