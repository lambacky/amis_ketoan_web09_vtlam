<template>
    <div class="toast-message-wrapper">
        <div class="toast-message-content">
            <div v-if="toastMessage.type == 'success'" class="toast-message-type">
                <div class="icon icon-success"></div>
                <div style="color:green">{{Text.success}}</div>
            </div>
            <div class="toast-message-text">{{toastMessage.message}}</div>
        </div>
        <div class="icon icon-close-small" @click="toggleToastMessage"></div>
    </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
import resourceVN from "../../resources/resourceVN"
export default {
    name:"BaseToastMessage",
    computed: mapGetters(["toastMessage","isShowToastMessage"]),
    created() {
        const me=this;
        setTimeout(function(){
            if(me.isShowToastMessage==true){
                me.toggleToastMessage();
            }
        }, 5500)
    },
    methods: {
        ...mapActions(["toggleToastMessage"]),
    },
    data() {
        return {
            Text:resourceVN.Text
        }
    },
}
</script>