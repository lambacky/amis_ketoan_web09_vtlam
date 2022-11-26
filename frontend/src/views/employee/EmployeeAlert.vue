<template>
    <BaseAlert :alert="alert" @confirmClick="confirmClick" @closeAll="closeAll"/>
</template>

<script>
import AlertAction from '@/enums/alertAction';
import { mapGetters, mapActions } from 'vuex'
import BaseAlert from '../../components/base/BaseAlert.vue'
export default {
    name:"EmployeeAlert",
    components:{BaseAlert},
    computed: mapGetters(["alert"]),
    emits: ["setIsStore"],
    methods: {
        ...mapActions([
            "toggleAlert",
            "deleteEmployee",
            "deleteBatchEmployee",
            "getEmployee",
            "toggleDialog",
        ]),

        /**
         * xác nhận cất hoặc xóa nhân viên
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        confirmClick(){
            const me=this;
            if(me.alert.action==AlertAction.CONFIRM_STORE){
                me.$emit("setIsStore");
            }
            if(me.alert.action==AlertAction.CONFIRM_DELETE){
                 me.deleteEmployee();
            }
            if(me.alert.action==AlertAction.CONFIRM_DELETE_BATCH){
                 me.deleteBatchEmployee();
            }
        },

        /**
         * chọn không xóa nhân viện
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        closeAll(){
            const me = this;
            me.toggleDialog();
        }
    },
}
</script>