<template>
  <b-card no-body class="post overflow-hidden mt-3" @click="ViewPostInfo">
    <b-row no-gutters>
      <b-col md="6">
        <b-card-img
          height="300"
          :src="image"
          alt="Image"
          class="rounded-0"
        ></b-card-img>
      </b-col>
      <b-col md="6">
        <b-card-body :title="title">
          <b-card-text>
            {{ content }}
          </b-card-text>
          <b-card-text class="small text-muted">{{
            this.getDatePost()
          }}</b-card-text>
        </b-card-body>
      </b-col>
    </b-row>
  </b-card>
</template>
<script>
export default {
  props: {
    Id: {
      type: Number,
      default: 0,
    },
    title: {
      type: String,
      default: "Default Title",
    },
    content: {
      type: String,
      default: "Default content",
    },
    createdAt: {
      type: String,
      default: "",
    },
    updatedAt: {
      type: String,
      default: "",
    },
    image: {
      type: String,
      default: "https://picsum.photos/id/1019/200/200",
    },
  },
  methods: {
    ViewPostInfo() {
      this.$router.push("/remote");
    },
    getDatePost() {
      if (this.updatedAt) {
        return this.updatedAt;
      }
      return this.createdAt;
    },
  },
};
</script>
<style lang="stylus">
.post
  cursor pointer
</style>
