<template>
  <b-button
    variant="primary"
    class="btn-circle btn-xl new-post"
    @click="$emit('create-post')"
  >
    <b-icon icon="plus-circle" scale="3" aria-hidden="true"></b-icon>
  </b-button>
</template>
<style lang="stylus">
.new-post
  position fixed
  top 20%
  left 95%
.btn-circle, .btn-xl
  width 70px
  height 70px
  padding 10px 16px
  border-radius 35px
  font-size 24px
  line-height 1.33
.new-post
  right 0
  top 90%
</style>
