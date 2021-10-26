<template>
  <b-container class="post-container">
    <ListPost />
  </b-container>
</template>
<script>
import blogType from "@/utils/blogType";
import ListPost from "@/components/Post/List";
import { mapMutations } from "vuex";
export default {
  name: "Remote",
  components: {
    ListPost,
  },
  mounted() {
    this.$store.commit("blog/SET_BLOG_TYPE", blogType.REMOTE);
  },
  methods: {
    ...mapMutations(["blog/SET_BLOG_TYPE"]),
  },
};
</script>
