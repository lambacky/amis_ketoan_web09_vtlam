<template>
    <div class="paging">
        <div class="paging-left">
            <div class="record-sum">{{Text.total}}: <b>{{totalRecord}}</b></div>
        </div>
        <div class="paging-right">
            <div class="page-limit">
                <BaseCombobox :dataList="pageSizeList" :className="'page-size-list'" :selectedItem="pageSize + Text.pageSize" @selectAction="selectPageSize"/>
            </div> 
            <div class="page-button">
                <button :disabled="pageNumber==1" class="btn-prev icon icon-arrow-left" @click="selectPageNumber(pageNumber-1)">
                </button>
                <div v-if="totalPage<6" class="total-page">
                    <div v-for="(page,index) in totalPage" :key="index" class="page-number" :class="{ 'selected': pageNumber == page }" @click="selectPageNumber(page)">{{page}}</div>
                </div>
                <div v-else class="total-page">
                    <div class="page-number" :class="{ 'selected': pageNumber == 1 }" @click="selectPageNumber(1)">1</div>
                    <div v-if="pageNumber>3" class="page-number" >...</div>

                    <div v-if="pageNumber==1||pageNumber==2" class="total-page">
                       <div class="page-number" :class="{ 'selected': pageNumber == 2 }" @click="selectPageNumber(2)">2</div>
                       <div class="page-number" @click="selectPageNumber(3)">3</div>
                       <div class="page-number" @click="selectPageNumber(4)">4</div>
                    </div>
                    <div v-else-if="pageNumber==totalPage||pageNumber==totalPage-1" class="total-page">
                       <div class="page-number" @click="selectPageNumber(totalPage-3)">{{totalPage-3}}</div>
                       <div class="page-number" @click="selectPageNumber(totalPage-2)">{{totalPage-2}}</div>
                       <div class="page-number" :class="{ 'selected': pageNumber == totalPage-1 }" @click="selectPageNumber(totalPage-1)">{{totalPage-1}}</div>
                    </div>
                    <div v-else class="total-page">
                        <div class="page-number" @click="selectPageNumber(pageNumber-1)">{{pageNumber-1}}</div>
                       <div class="page-number" :class="'selected'">{{pageNumber}}</div>
                       <div class="page-number" @click="selectPageNumber(pageNumber+1)">{{pageNumber+1}}</div>
                    </div>
                    <div v-if="pageNumber<totalPage-2" class="page-number" >...</div>
                    <div class="page-number" :class="{ 'selected': pageNumber == totalPage }" @click="selectPageNumber(totalPage)">{{totalPage}}</div>
                </div>
                <button :disabled="pageNumber==totalPage" class="btn-next icon icon-arrow-right" @click="selectPageNumber(pageNumber+1)"></button>
            </div>                  
        </div>
    </div>
</template>

<script>
import BaseCombobox from '../../components/base/BaseCombobox.vue'
import resourceVN from "../../resources/resourceVN"
export default {
    name:"BaseInput",
    components:{BaseCombobox},
    props:["totalRecord","pageSize","pageNumber","totalPage"],
    emits:["selectPageSize","selectPageNumber"],
    methods:{
        selectPageNumber(pageNumber){
            this.$emit("selectPageNumber",pageNumber);
        },
        selectPageSize(pageSize){
            this.$emit("selectPageSize",pageSize);
        }
    },
    data() {
        return {
            Text: resourceVN.Text,
            pageSizeList:['10','20','30','50','100'].map(item => item + resourceVN.Text.pageSize)
        }
    },
}
</script>