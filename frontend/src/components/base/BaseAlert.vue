<template>
    <div class="dialog-wrapper alert">
        <div class="dialog alert-dialog">
            <div class="dialog-body alert-body">
                <div v-if="alert.type=='warning'" class="icon icon-warning"></div>
                <div v-if="alert.type=='danger'" class="icon icon-danger"></div>
                <div v-if="alert.type=='question'" class="icon icon-question"></div>
                <div v-if="alert.type=='success'" class="icon icon-success"></div>

                <div class="content-message">{{alert.message}}</div>
            </div>

            <div v-if="alert.type=='warning'" class="dialog-footer alert-warning-delete-footer">
                <BaseButton :btnText="Text.deny" :isSecondary="true" @click="toggleAlert" tabIndex="2" />
                <BaseButton :btnText="Text.confirm" @click="confirmClick" tabIndex="1" />
            </div>

            <div v-if="alert.type=='danger'||alert.type=='success'" class="dialog-footer alert-danger-footer">
                <BaseButton :btnText="Text.close" @click="toggleAlert" tabIndex="1" />
            </div>

            <div v-if="alert.type=='question'" class="dialog-footer alert-question-footer">
                <BaseButton :btnText="Text.cancel" :isSecondary="true" @click="toggleAlert" tabIndex="3" />
                <div class="btn-action">
                    <BaseButton :btnText="Text.deny" :isSecondary="true" @click="closeAll" tabIndex="2" />
                    <BaseButton :btnText="Text.confirm" @click="confirmClick" tabIndex="1" />
                </div>
            </div>

        </div>
    </div>
</template>

<script>
import { mapActions } from 'vuex'
import BaseButton from './BaseButton.vue'
import resourceVN from "../../resources/resourceVN"
export default {
    name:"EmployeeAlert",
    components:{BaseButton},
    props:["alert"],
    emits: ["confirmClick","closeAll"],
    methods: {
        ...mapActions([
            "toggleAlert",
        ]),

        /**
         * xác nhận cất hoặc xóa dữ liệu
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        confirmClick(){
            const me=this;
            me.toggleAlert();
            me.$emit("confirmClick");
        },

        /**
         * chọn không xóa dữ liệu
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        closeAll(){
            const me = this;
            me.toggleAlert();
            me.$emit("closeAll");
        }
    },
    data() {
        return {
            Text: resourceVN.Text
        }
    },
}
</script>