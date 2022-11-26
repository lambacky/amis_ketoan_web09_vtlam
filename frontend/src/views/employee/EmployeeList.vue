<template>
    <div class="main-content">
        <div class="content-header">
            <div class="content-title">{{Text.employee}}</div>
            <BaseButton :btnText="Text.addEmployee" @click="openForm"/>
        </div>
        
        <div class="content-body">
            <div class="toolbar">
                <div class="toolbar-left">
                    <div class="batch-action" @click="toggleList">
                        <div>{{Text.batchAction}}</div>
                        <div class="icon icon-arrow-dropdown"></div>
                    </div>
                    <div v-if="isShowList" class="batch-action-list">
                        <div v-if="checkedEmployeeIds.length > 0" @click="deleteBatch" class="dropdown-item">{{Text.delete}}</div>
                    </div>
                </div>
                <div class="toolbar-right">
                    <div class="search-area">
                        <BaseInput @keyup="searchEmployee" v-model="filter.employeeFilter" :placeHolder="Text.searchPlaceHolder" :icon="'icon-search'"/>
                    </div>
                    <button @click="refreshTable" class="btn-refresh icon icon-refresh" :title="Text.refreshToolTip"></button>
                    <button @click="exportToExcel" class="btn-excel icon icon-excel" :title="Text.exportToolTip"></button>
                </div>
            </div>

            <EmployeeTable />

            <EmployeePaging />
        </div>
    </div>

    <!-- FORM DIALOG -->
    <EmployeeDialog v-if="isShowDialog" :isStore="isStore"
      @isStoreDone="() => (this.isStore = false)"/>

    <EmployeeAlert v-if="isShowAlert" @setIsStore="() => (this.isStore = true)"/>
</template>

<script>

import  FormMode  from "../../enums/formMode.js"
import Gender from "../../enums/gender.js"
import AlertAction from "../../enums/alertAction.js"
import { mapActions, mapGetters } from "vuex"
import EmployeeDialog from './EmployeeDialog.vue'
import EmployeeTable from './EmployeeTable.vue'
import EmployeePaging from './EmployeePaging.vue'
import EmployeeAlert from './EmployeeAlert.vue'
import BaseButton from '../../components/base/BaseButton.vue'
import BaseInput from '../../components/base/BaseInput.vue'
import resourceVN from "../../resources/resourceVN"

export default {
    name:"EmployeeList",
    components:{EmployeeTable, EmployeePaging, EmployeeDialog, EmployeeAlert,BaseButton,BaseInput},
    props:[],
    computed: mapGetters([
        "isShowDialog",
        "isShowAlert",
        "filter",
        "dialogTitle",
        "checkedEmployeeIds"
        ]),
    created() {
        const me = this;
        //load dữ liệu nhân viên và phòng ban
        me.getEmployee();
        me.getDepartment();
    },
    methods:{
        ...mapActions([
            "changeFormMode",
            "toggleDialog",
            "toggleLoading",
            "getEmployee",
            "getDepartment",
            "selectEmployee",
            "setFilter",
            "setDialogTitle",
            "setAlert",
            "exportToExcel"
            
        ]),
        
        /**
         * Mở form nhân viên
         * Author:Vũ Tùng Lâm (30/10/2022)
         */
        openForm(){
            const me = this;
            me.setDialogTitle(resourceVN.Text.addFormTitle);
            me.selectEmployee({ Gender: Gender.MALE });
            me.changeFormMode(FormMode.STORE);
            me.toggleDialog();            
        },

        /**
         * Tìm kiếm nhân viên
         * Author:Vũ Tùng Lâm (30/10/2022)
         */
        searchEmployee(){
            const me = this;   
            if (me.timer) {
                clearTimeout(me.timer);
                me.timer = null;
            }
            me.timer = setTimeout(() => {
                me.setFilter({
                    pageSize: me.filter.pageSize,
                    pageNumber: 1,
                    employeeFilter: me.filter.employeeFilter
                });
                me.getEmployee();
            }, 500);
        },

        /**
         * refresh dữ liệu danh sách nhân viên
         * Author:Vũ Tùng Lâm (30/10/2022)
         */
        refreshTable(){
            const me = this;
            me.setFilter({
                pageSize: me.filter.pageSize,
                pageNumber: 1,
                employeeFilter: ""
            });
            me.getEmployee();
        },

        /**
         * Xóa hàng loạt
         * Author:Vũ Tùng Lâm (30/10/2022)
         */
        deleteBatch(){
            const me=this;
            me.setAlert({
                type:"warning",
                message: resourceVN.AlertMessage.deleteBatchWarning,
                action: AlertAction.CONFIRM_DELETE_BATCH
            });
        },

        /**
         * Ẩn/hiện list
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        toggleList(){
            const me=this;
            me.isShowList=!me.isShowList;
        }
    },
    data() {
        return {
            isStore: false,
            isShowList:false,
            Text:resourceVN.Text
        };
    },
    
}
</script>