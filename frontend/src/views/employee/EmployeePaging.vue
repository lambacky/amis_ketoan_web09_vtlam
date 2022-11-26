<template>
    <BasePaging :pageNumber="filter.pageNumber" :pageSize="filter.pageSize" :totalRecord="totalEmployee" :totalPage="totalPage"
    @selectPageNumber="selectPageNumber" @selectPageSize="selectPageSize"/>
</template>

<script>
import { mapGetters,mapActions } from 'vuex'
import BasePaging from '../../components/base/BasePaging.vue'
export default {
    name:"EmployeePaging",
    components:{BasePaging},
    computed: mapGetters([
        "totalEmployee",
        "totalPage",
        "filter",
    ]),
    methods: {
        ...mapActions(["setFilter", "getEmployee"]),

        /**
         * chọn số trang
         * @param {int} pageNumber
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        selectPageNumber(pageNumber){
            const me = this;
            me.setFilter({
                pageSize: me.filter.pageSize,
                pageNumber: pageNumber,
                employeeFilter: me.filter.employeeFilter
            });
            me.getEmployee();
        },
        
        /**
         * chọn số lượng bản ghi mỗi trang
         * @param {int} pageSize
         * Author: Vũ Tùng Lâm (30/10/2022)
         */
        selectPageSize(pageSize){
            pageSize = pageSize.split(' ')[0];
            const me = this;
            me.setFilter({
                pageSize: pageSize,
                pageNumber: 1,
                employeeFilter: me.filter.employeeFilter
            });
            me.getEmployee();
        },
    }
}
</script>