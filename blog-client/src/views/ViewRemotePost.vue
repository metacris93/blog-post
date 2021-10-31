<template>
  <main v-if="this.post">
    <h1>{{ getTitle }}</h1>
    <figure>
      <img :src="getImage" alt="Image Post" />
    </figure>
    <div class="post-date d-flex justify-content-around">
      <div>
        <em>{{ getCreatedAt }}</em>
      </div>
      <div v-if="hasPostUpdated">
        <em>{{ showIfPostWasUpdated }}</em>
      </div>
    </div>
    <article>
      {{ getContent }}
    </article>
  </main>
  <main v-else>
    <h1>Post No Encontrado</h1>
  </main>
</template>
<script>
import moment from "moment";
import { getPost } from "@/api/post";
export default {
  data() {
    return {
      defaultImage: "https://picsum.photos/id/1019/200/200",
      post: null,
    };
  },
  computed: {
    getTitle() {
      return this.post.Title ?? "";
    },
    getCreatedAt() {
      if (this.post.Created_at)
        return `Creado en: ${moment(this.post.Created_at)
          .locale("es")
          .calendar()}`;
      return "";
    },
    getContent() {
      return this.post.Content ?? "";
    },
    getImage() {
      if (this.post.Image) return this.post.Image;
      return this.defaultImage;
    },
    hasPostUpdated() {
      return this.post.Updated_at ?? false;
    },
    showIfPostWasUpdated() {
      return `Actualizado en: ${moment(this.post.Updated_at)
        .locale("es")
        .calendar()}`;
    },
  },
  async mounted() {
    const post = await getPost(this.$route.params.id);
    if (post) {
      this.post = post.data;
    }
  },
};
</script>
<style lang="stylus">
h1
  padding-top 2.5rem
img
  margin-top 1rem
.post-date
  margin-top 1.5rem
article
  margin-top 2rem
  font-size 1.5em
  font-family Trirong, sans-serif
</style>
