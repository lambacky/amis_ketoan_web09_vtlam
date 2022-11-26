<template>
    <div class="table-wrapper">
        <table>
            <thead>
                <tr>
                    <th>
                        <input id="checkboxAll" @change="checkAll" class="input-checkbox" type="checkbox">
                    </th>
                    <th v-for="(header,index) in headers" :key="index" :class="header.class" :style="{'min-width':header.minWidth}" :title="header.title" >{{header.name}}</th>
                    <th class="function" style="min-width:120px">{{Text.function}}</th>
                </tr>
            </thead>
            <tbody v-if="isShowLoading">
                <tr v-for="index in 8" :key="index">
                    <td>
                        <input ref="checkbox" @change="checkOne(data,index)" class="input-checkbox" type="checkbox">
                    </td>
                    <td v-for="index in headers.length" :key="index">
                        <div class="loading"></div>
                    </td>
                    <td><div class="loading"></div></td>
                </tr>
            </tbody>
            <tbody v-else>
                <tr v-for="(data,index) in dataList" :key="index" :class="{'row-checked': checkedRows[index]==true}" @click="rowClick(index)" @dblclick="modifyForm(data)">
                    <td @click.stop @dblclick.stop>
                        <input ref="checkbox" @change="checkOne(data,index)" class="input-checkbox" type="checkbox">
                    </td>
                    <td v-for="(value,index) in mapData(data)" :key="index" :class="headers[index].class">{{value}}</td>
                    <td @dblclick.stop @click.stop :style="{'z-index': dataList.length-index}">
                        <div class="table-function">
                            <div class="modify" @click="modifyForm(data)">{{Text.modify}}</div>
                            <div class="dropdown context-menu">
                                <div class="icon dropdown-button icon-arrow-down-blue" @click="toggleList"></div>
                                <div class="dropdown-list">
                                    <div @click="selectDuplicate(data)" class="dropdown-item">{{Text.duplicate}}</div>
                                    <div @click="selectDelete(data)" class="dropdown-item delete-item">{{Text.delete}}</div>
                                    <div class="dropdown-item">{{Text.stopUsing}}</div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import {mapGetters} from "vuex"
import resourceVN from "../../resources/resourceVN"
export default {
    name:"BaseTable",
    props:["headers","dataList","mapData"],
    emits:["modifyForm","selectDuplicate","selectDelete","checkAll","checkOne"],
    computed: mapGetters(["isShowLoading"]),
    methods:{
        /**
         * chỉnh sửa dữ liệu
         * @param {*} data
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        modifyForm(data){
            this.$emit("modifyForm",data);
        },

        /**
         * chọn nhân bản
         * @param {*} data
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        selectDuplicate(data){
            this.$emit("selectDuplicate",data);
        },

        /**
         * chọn xóa dữ liệu
         * @param {*} data
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        selectDelete(data){
            this.$emit("selectDelete",data);
        },

        /**
         * check tất cả checkbox
         * @param {*} event
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        checkAll(event){
            const me=this;
            let checkboxes = document.querySelectorAll("tbody input");
            checkboxes.forEach(checkbox=>{checkbox.checked=event.target.checked});
            me.checkedRows = new Array(me.dataList.length).fill(event.target.checked);
            me.$emit("checkAll",event);
        },

        /**
         * check từng checkbox
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        checkOne(data,index){
            const me=this;
            document.querySelector('#checkboxAll').checked = (document.querySelectorAll("tbody input:checked").length==document.querySelectorAll("tbody input").length);
            me.$emit("checkOne",data);
            me.checkedRows[index] = !me.checkedRows[index];
        },

        /**
         * Ẩn/hiện dropdown
         * @param {*} event
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        toggleList(event){
            let dropdownList = event.target.nextElementSibling;
            if(dropdownList.style.display=="block"){
                dropdownList.style.display="none";
            }else{
                dropdownList.style.display="block";
            }
        },

        /**
         * click vào từng hàng
         * @param {*} index
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        rowClick(index){
            const me=this;
            me.$refs["checkbox"][index].click();
        }
    },
    mounted(){
        /**
         * Ẩn dropdown khi click bên ngoài
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        document.addEventListener('click',function(event){
            document.querySelectorAll(".dropdown-list").forEach(item => {
                if ((event.target!=item.previousElementSibling)) {
                    item.style.display="none";
                }
            });
        });
    },
    data() {
        return {
            Text:resourceVN.Text,
            checkedRows:Array(this.dataList.length).fill(false),
        }
    },
}
</script>