<template>
  <b-container class="post-container">
    <ListPost />
  </b-container>
</template>
<script>
import blogType from "@/utils/blogType";
import { mapMutations } from "vuex";
import ListPost from "@/components/Post/List";
export default {
  name: "RemotePlus",
  components: {
    ListPost,
  },
  mounted() {
    this.$store.commit("blog/SET_BLOG_TYPE", blogType.REMOTE_PLUS);
  },
  methods: {
    ...mapMutations(["blog/SET_BLOG_TYPE"]),
  },
};
</script>
