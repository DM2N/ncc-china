<template>
  <div id="hotuser">
    <el-card
      class="box-card"
      shadow="hover"
    >
      <div
        slot="header"
        class="clearfix"
      >
        <span><nuxt-link to="/">主页</nuxt-link> / 热门用户</span>
      </div>
      <div
        v-for="(hotuser, index) in hotusers"
        :key="index"
        index="index"
        class="user-list"
      >
        <img
          :src="hotuser.ava"
          alt=""
          class="wh30 round vertical-middle"
        >
        <span>{{ hotuser.name }}</span>
        <span>{{ hotuser.jifen }}</span>
      </div>
    </el-card>
  </div>
</template>

<script>
export default {
  computed: {
    hotusers() {
      return [
        {
          ava: require('../../static/test.jpg'),
          name: 'test',
          jifen: 100
        }
      ]
    }
  }
}
</script>

<style scoped>
.user-list span {
  margin-left: 20px;
}
</style>
